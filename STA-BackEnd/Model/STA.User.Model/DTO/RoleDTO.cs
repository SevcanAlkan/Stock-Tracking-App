using Microsoft.AspNetCore.Identity;
using STA.Core.Enum;
using System;

namespace STA.User.Model.DTO
{
    public class RoleBase : IdentityRole<Guid>
    {

    }

    public class RoleDTO : RoleBase
    {
        //Foreign keys
        public RoleDTO()
        {
        }
    }
}
