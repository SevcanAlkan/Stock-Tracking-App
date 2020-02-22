using EventBus.Base.Standard;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.EventBus.Events
{
    public class UserRegistrationEvent : IntegrationEvent
    {
        public User UserDTO { get; set; }

        public UserRegistrationEvent(User userDTO)
        {
            this.UserDTO = userDTO;
        }
    }
}
