
namespace Kolejki.API.Services
{
    public interface IPaypalService
    {
        Task<string> GetToken();
        Task<string> CreateOrder();
    }
}