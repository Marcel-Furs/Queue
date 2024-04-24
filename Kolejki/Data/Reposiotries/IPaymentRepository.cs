using Kolejki.API.Data.Models;

namespace Kolejki.API.Data.Reposiotries
{
    public interface IPaymentRepository
    {
        public void Add(Payment payment);
    }
}
