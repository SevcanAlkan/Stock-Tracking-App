using EventBus.Base.Standard;
using NGA.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NGA.EventBus.Handlers
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
