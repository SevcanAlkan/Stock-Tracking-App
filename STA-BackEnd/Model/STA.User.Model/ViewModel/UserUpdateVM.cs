using STA.Core.Enum;
using STA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace STA.User.Model.ViewModel
{
    public class UserUpdateVM : UpdateVM
    {
        [Required, MaxLength(15)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string PasswordHash { get; set; }
        public string OldPassword { get; set; }


        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
        [DefaultValue(false)]
        public bool IsBanned { get; set; }

        [Required, MaxLength(20)]
        public string DisplayName { get; set; }
        [MaxLength(250)]
        public string About { get; set; }

        [Required, Range(1, 4), DefaultValue(1)]
        public UserStatus Status { get; set; }
    }
}
