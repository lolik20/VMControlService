using EventBus.Base.Standard;
using HyperV.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyperV.API
{
    public class EventHandler : IIntegrationEventHandler<Event>
    {
        public EventHandler()
        {

        }
        public async Task Handle(Event @event)
        {
            
        }

    }
}
