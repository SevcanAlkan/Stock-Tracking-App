using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace STA.EventBus.Model
{
    public class AuthenticateModel
    {
        [Required, MaxLength(15)]
        public string Username { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
    }
}
