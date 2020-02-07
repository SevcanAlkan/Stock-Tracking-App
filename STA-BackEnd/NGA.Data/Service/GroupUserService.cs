using AutoMapper;
using Microsoft.Extensions.Logging;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;

namespace NGA.Data.Service
{
    public class GroupUserService : BaseService<GroupUserAddVM, GroupUserUpdateVM, GroupUserVM, GroupUser>, IGroupUserService
    {
        #region Ctor

        public GroupUserService(UnitOfWork _uow, IMapper _mapper, ILogger<BaseService<GroupUserAddVM, GroupUserUpdateVM, GroupUserVM, GroupUser>> _logger)
            : base(_uow, _mapper, _logger)
        {

        }

        #endregion

        #region Methods                


        #endregion
    }

    public interface IGroupUserService : IBaseService<GroupUserAddVM, GroupUserUpdateVM, GroupUserVM, GroupUser>
    {
    }
}
