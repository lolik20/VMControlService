using EventBus.Base.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace HyperV.BL
{
    public class Event : IntegrationEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Event(string title, string description)
        {
            Title = title;
            Description = description;
        }

    }
}
