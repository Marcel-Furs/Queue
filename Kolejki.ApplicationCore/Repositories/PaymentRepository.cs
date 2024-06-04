using Kolejki.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolejki.ApplicationCore.Repositories
{
    public class PaymentRepository : IRepository<Payment>
    {
        private List<Payment> payments;

        public PaymentRepository()
        {
            payments = new List<Payment>();
        }

        public async Task<Payment> Add(Payment entity)
        {
            if(payments.Count == 0)
            {
                entity.Id = 1.ToString();
            } else
            {
                entity.Id = (int.Parse(payments.Last().Id) + 1).ToString();
            }
            payments.Add(entity);
            return entity;
        }

        public async Task Delete(string id)
        {
            Payment entity = await Get(id);
            if(entity != null)
            {
                payments.Remove(entity);
            }
        }

        public async Task<Payment?> Get(string id)
        {
            return payments.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            return payments;
        }

        public async Task<Payment?> Update(Payment entity)
        {
            Payment? original = await Get(entity.Id);
            if (original != null)
            {
                original.BuyerEmail = entity.BuyerEmail;
                original.PaypalId = entity.PaypalId;
                original.Amount = entity.Amount;
                original.PaymentStatus = entity.PaymentStatus;
            }
            return original;
        }
    }
}
