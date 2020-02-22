using Newtonsoft.Json;
using STA.Core.Enum;
using STA.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.User.Model.ViewModel
{
    public class UserVM : BaseVM
    {
        public string UserName { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }

        public string DisplayName { get; set; }
        public string About { get; set; }

        [JsonIgnore]
        public UserStatus Status { get; set; }
        public int StatusVal
        {
            get
            {
                return (int)this.Status;
            }
        }
    }
}
