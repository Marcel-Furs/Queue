using Kolejki.ApplicationCore.Exceptions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Kolejki.API.Services
{
    internal class Token
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }

    internal class CreateOrderResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public record PaypalService(IHttpClientFactory HttpClientFactory) : IPaypalService
    {
        public async Task<string> GetToken()
        {
            Dictionary<string, string> form = new()
            {
                { "grant_type", "client_credentials" }
            };
            var httpClient = HttpClientFactory.CreateClient("GetToken");
            var response = await httpClient.PostAsync("", new FormUrlEncodedContent(form));
            var token = await response.Content.ReadFromJsonAsync<Token>();
            return token.AccessToken;
        }

        public async Task<string> CreateOrder()
        {
            var order = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                    new
                    {
                        amount = new
                        {
                            currency_code = "PLN",
                            value = "500"
                        }
                    }
                }
            };
            string token = await GetToken() ?? throw new NullReferenceException("Token is null");
            var httpClient = HttpClientFactory.CreateClient("CreateOrder");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsJsonAsync("", order);
            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<CreateOrderResponse>();
                return responseBody.Id;
            }
            else
            {
                throw new OrderCreationException("Invalid paypal response: " + response.StatusCode);
            }
        }
    }
}

