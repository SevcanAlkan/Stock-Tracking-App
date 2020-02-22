using EventBus.Base.Standard;
using STA.EventBus.Model;

namespace STA.EventBus.Events
{
    public class UserRegistrationEvent : IntegrationEvent
    {
        public UserRegistrationModel User { get; set; }

        public UserRegistrationEvent(UserRegistrationModel user)
        {
            this.User = user;
        }
    }
}
