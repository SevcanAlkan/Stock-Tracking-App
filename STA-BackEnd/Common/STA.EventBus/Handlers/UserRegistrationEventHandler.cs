using EventBus.Base.Standard;
using STA.EventBus.Events;
using System.Threading.Tasks;

namespace STA.EventBus.Handlers
{
    public class UserRegistrationEventHandler : IIntegrationEventHandler<UserRegistrationEvent>
    {
        public UserRegistrationEventHandler()
        {
        }

        public async Task Handle(UserRegistrationEvent @event)
        {
            //Handle the ItemCreatedIntegrationEvent event here.
        }
    }
}
