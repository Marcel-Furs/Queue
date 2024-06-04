using Kolejki.API.Services;
using Kolejki.ApplicationCore.Exceptions;
using Kolejki.ApplicationCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Kolejki.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMQTTService mqttService;
        private readonly IPaypalService paypalService;
        private readonly IPaymentService paymentService;

        public PaymentController(IMQTTService mqttService, IPaypalService paypalService, IPaymentService paymentService)
        {
            this.mqttService = mqttService;
            this.paypalService = paypalService;
            this.paymentService = paymentService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> EmailTest([FromBody]string email)
        {
            //mqttService.SendEmail(email, "Powitanie", await paypalService.GetToken());
            //await paypalService.CreateOrder();
            //mqttService.SendEmail(email, "Powitanie", "Witaj na naszej stronie");

            try
            {
                var paypalId = await paypalService.CreateOrder();
                await paymentService.CreatePayment(paypalId, email, 500);
                return Ok(PaypalIdDto.Of(paypalId));
            } catch (OrderCreationException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [HttpPost("approve-order")]
        public async Task<IActionResult> ApproveOrder([FromBody] ApprovedPaymentDto approvedPaymentDto)
        {
            var order = await paymentService.ChangePaymentStatusByPaypalId(approvedPaymentDto.OrderID, "Success");
            return Ok(order); //TODO: dorobic DTO
        }
    }
}
