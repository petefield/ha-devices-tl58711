using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheFields.Iot.Devices.SPI.Contracts;

namespace TL59711.Controllers
{
    public class LedState
    {
        public int Channel {get; set;}
        public double Brightness { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class LedsController : ControllerBase
    {
        private readonly ILogger<LedsController> _logger;
        private readonly ITL59711 _TL59711;

        public LedsController(ILogger<LedsController> logger, ITL59711 TL59711 )
        {
            _logger = logger;
            _TL59711 = TL59711;
        }

        [HttpPost("{led}/{brightness}")]
        public void state(int led, double brightness)
        {
            _TL59711.SetBrightness(led, brightness);
        }

        [HttpPost]
        public void state(LedState[] states)
        {
            foreach (var state in states)
            {
                _TL59711.SetBrightness(state.Channel, state.Brightness);
            }
        }
    }
}
