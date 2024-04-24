using Kolejki.API.Data.Models;

namespace Kolejki.API.Data.Reposiotries
{
    //public class PaymentReposiotry(DataContext dataContext)
    //{
    //    private readonly DataContext dataContext = dataContext;
        
    //    public void Add(Payment payment)
    //    {
    //        dataContext.Add(payment);
    //        dataContext.SaveChanges();
    //    }
    //}

    public record PaymentReposiotry(DataContext DataContext) : IPaymentRepository
    {
        public void Add(Payment payment)
        {
            DataContext.Add(payment);
            DataContext.SaveChanges();
        }
    }
}
