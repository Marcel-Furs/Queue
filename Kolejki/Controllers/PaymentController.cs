using Kolejki.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolejki.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMQTTService mqttService;
        private readonly IPaypalService paypalService;

        public PaymentController(IMQTTService mqttService, IPaypalService paypalService)
        {
            this.mqttService = mqttService;
            this.paypalService = paypalService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> EmailTest([FromBody]string email)
        {
            //mqttService.SendEmail(email, "Powitanie", await paypalService.GetToken());
            //await paypalService.CreateOrder();
            //mqttService.SendEmail(email, "Powitanie", "Witaj na naszej stronie");
            return Ok(new { orderId = await paypalService.CreateOrder() });
        }
    }
}
