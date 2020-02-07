using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NGA.Core.Validation;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGA.Data.Service
{
    public class GroupService : BaseService<GroupAddVM, GroupUpdateVM, GroupVM, Group>, IGroupService
    {
        #region Ctor
        private IGroupUserService groupUserService;

        public GroupService(UnitOfWork _uow, IMapper _mapper, IGroupUserService _groupUserService, ILogger<BaseService<GroupAddVM, GroupUpdateVM, GroupVM, Group>> _logger)
            : base(_uow, _mapper, _logger)
        {
            this.groupUserService = _groupUserService;
        }

        #endregion

        #region Methods                

        public List<GroupVM> GetByUserId(Guid userId)
        {
            if (Validation.IsNullOrEmpty(userId))
            {
                return new List<GroupVM>();
            }

            var groups = this.Repository.Query().Where(a => !a.IsPrivate || (a.Users.Any(x => x.UserId == userId))).ToList().Select(a => new GroupVM()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                IsPrivate = a.IsPrivate,
                IsMain = a.IsMain,
                IsOneToOneChat = a.IsOneToOneChat,
                Users = groupUserService.Repository.Query().Where(x => x.GroupId == a.Id).Select(s => s.UserId).ToList()
            }).ToList();

            return groups;
        }

        public List<Guid> GetUsers(Guid groupId)
        {
            if (Validation.IsNullOrEmpty(groupId))
            {
                return new List<Guid>();
            }

            var users = groupUserService.Repository.Query().Where(a => a.GroupId == groupId).Select(a => a.UserId).ToList();

            return users;
        }

        public override Task<GroupVM> GetById(Guid id)
        {
            return Repository.Query().Where(x => x.Id == id).Include("Users").Select(s => mapper.Map<Group, GroupVM>(s)).FirstOrDefaultAsync();
        }

        public override async Task<Group> Add(GroupAddVM model, Guid? userId = null, bool isCommit = true)
        {
            if (model.IsMain)
            {
                model.IsMain = false;
            }

            var result = await base.Add(model, userId, true);
            if (result.IsNull())
                return result;

            if (model.Users != null && model.Users.Count > 0)
            {
                foreach (var id in model.Users)
                {
                    GroupUserAddVM item = new GroupUserAddVM();
                    item.GroupId = result.Id;
                    item.UserId = id;


                    var userResult = await groupUserService.Add(item, userId, true);
                    if (userResult.IsNull())
                        continue;
                }
            }

            return result;
        }

        public override async Task<Group> Update(Guid id, GroupUpdateVM model, Guid? userId = null, bool isCommit = true)
        {
            if (model.IsMain && Repository.Query().Any(a => a.Id != id && a.IsMain))
                return null;


            var result = await base.Update(id, model, userId, true);
            if (result.IsNull())
                return result;

            if (model.Users != null && model.Users.Count > 0)
            {
                foreach (var item in model.Users)
                {
                    var groupUser = groupUserService.GetById(item);
                    if (groupUser == null)
                    {
                        GroupUserAddVM rec = new GroupUserAddVM();
                        rec.GroupId = id;
                        rec.UserId = item;

                        var userResult = await groupUserService.Add(rec, userId, true);
                        if (userResult.IsNull())
                            continue;
                    }
                }
            }

            var removedUsers = groupUserService.Repository.Query().Where(a => a.GroupId == id && (model.Users == null || !model.Users.Any(x => x == a.UserId))).Select(a => a.Id).ToList();
            if (removedUsers != null)
            {
                foreach (var item in removedUsers)
                {
                    await groupUserService.Delete(item, userId, true);
                }
            }

            return result;
        }

        public override async Task<bool> Delete(Guid id, Guid? userId = null, bool isCommit = true)
        {
            var group = await GetById(id);
            if (group == null || group.IsMain)
                return false;


            var result = await base.Delete(id, userId, true);
            if (result.IsNull())
                return result;

            var users = groupUserService.Repository.Query().Where(a => a.GroupId == id).Select(a => a.Id).ToList();
            if (users != null)
            {
                foreach (var item in users)
                {
                    await groupUserService.Delete(item, userId, true);
                }
            }


            return result;
        }

        #endregion
    }

    public interface IGroupService : IBaseService<GroupAddVM, GroupUpdateVM, GroupVM, Group>
    {
        List<GroupVM> GetByUserId(Guid userId);
        List<Guid> GetUsers(Guid groupId);
    }
}
