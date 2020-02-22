using STA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace STA.EventBus.Model
{
    public class TokenVM
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public bool IsAdmin { get; set; }

        public string DisplayName { get; set; }
        public string About { get; set; }

        public string Token { get; set; }
    }
}
