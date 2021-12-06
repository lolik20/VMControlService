using EventBus.Base.Standard;
using MainApplication.BL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApplication.API.Controllers
{
    [AllowAnonymous]
    [Route("api/test")]
    [ApiController]
    public class TestEventController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        public TestEventController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        [HttpPost("send")]
        public IActionResult Test([FromBody ] EventRequest request)
        {
            try
            {
                var @event = new Event(request.Title, request.Description);
                 _eventBus.Publish(@event);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
    public class EventRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
