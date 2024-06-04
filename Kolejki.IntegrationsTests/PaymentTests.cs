using Microsoft.AspNetCore.Mvc.Testing;
using Shared;
using Xunit;

namespace Kolejki.IntegrationsTests
{
    public class PaymentTests
    {
        [Fact]
        public async void CreateOrderShouldReturnPaypalId()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var client = app.CreateClient();

            var response = await client.PostAsJsonAsync("/api/payment/create-order", "tester@gmail.com");
            response.EnsureSuccessStatusCode();

            var paypalObj = await response.Content.ReadFromJsonAsync<PaypalIdDto>();
            Assert.NotNull(paypalObj);
            Assert.NotNull(paypalObj.PaypalId);
        }
    }
}