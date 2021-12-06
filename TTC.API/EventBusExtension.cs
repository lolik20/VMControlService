using EventBus.Base.Standard;
using HyperV.BL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyperV.API
{
    public static class EventBusExtension
    {
        public static IEnumerable<IIntegrationEventHandler> GetHandlers()
        {
            return new List<IIntegrationEventHandler>
            {
                new EventHandler()
            };

        }
        public static IApplicationBuilder SubscribeToEvent(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<Event, EventHandler>();
            return app;
        }
    }
}
