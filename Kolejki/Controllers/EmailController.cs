using Kolejki.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolejki.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IMQTTService mqttService;

        public EmailController(IMQTTService mqttService)
        {
            this.mqttService = mqttService;
        }

        [HttpPost]
        public IActionResult EmailTest([FromBody]string email)
        {
            mqttService.SendEmail(email, "Powitanie", "Witaj na naszej stronie");
            return Ok();
        }
    }
}
