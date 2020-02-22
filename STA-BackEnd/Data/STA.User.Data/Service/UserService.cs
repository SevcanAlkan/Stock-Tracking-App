using AutoMapper;
using Microsoft.Extensions.Logging;
using STA.Data.SubStructure;
using STA.User.Model.DTO;
using STA.User.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STA.User.Data.Service
{
    public class UserService : IUserService
    {
        private UserDbContext con;
        private readonly IMapper mapper;

        #region Ctor

        public UserService(UserDbContext _con, IMapper _mapper)
        {
            con = _con;
            mapper = _mapper;
        }

        #endregion

        #region Methods

        public List<UserVM> GetAll()
        {
            var userList = con.Set<UserDTO>().ToList();
            List<UserVM> result = mapper.Map<List<UserVM>>(userList);

            return result;
        }

        public bool Any(string userName)
        {
            var result = con.Set<UserDTO>().Any(a => a.UserName == userName);

            return result;
        }

        public UserDTO GetById(Guid id)
        {
            var rec = con.Set<UserDTO>().Where(a => a.Id == id).FirstOrDefault();

            if (rec == null)
            {
                return null;
            }

            rec.AccessFailedCount = 0;
            rec.ConcurrencyStamp = "";
            //rec.PasswordHash = "";
            rec.SecurityStamp = "";

            return rec;
        }

        #endregion
    }

    public interface IUserService
    {
        List<UserVM> GetAll();
        bool Any(string userName);
        UserDTO GetById(Guid id);
    }
}
