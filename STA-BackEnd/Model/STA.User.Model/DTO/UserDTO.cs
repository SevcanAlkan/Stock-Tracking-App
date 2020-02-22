using Microsoft.AspNetCore.Identity;
using STA.Core.EntityFramework;
using STA.Core.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace STA.User.Model.DTO
{
    public class UserBase : IdentityUser<Guid>, IBase
    {
        public DateTime? LastLoginDateTime { get; set; }

        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
        [DefaultValue(false)]
        public bool IsBanned { get; set; }

        [Required, MaxLength(20)]
        public string DisplayName { get; set; }
        [MaxLength(300)]
        public string About { get; set; }

        [Required, DefaultValue(UserStatus.Offline)]
        public UserStatus Status { get; set; }
        [Required]
        public DateTime CreateDateTime { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }

    public class UserDTO : UserBase
    {
        //Foreign keys
        public UserDTO()
        {
        }
    }
}
