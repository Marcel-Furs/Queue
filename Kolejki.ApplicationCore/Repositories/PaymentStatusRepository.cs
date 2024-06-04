using Kolejki.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolejki.ApplicationCore.Repositories
{
    public class PaymentStatusRepository : IRepository<PaymentStatus>
    {
        private List<PaymentStatus> paymentStatuses;

        public PaymentStatusRepository()
        {
            paymentStatuses = new List<PaymentStatus>()
            {
                new PaymentStatus {Id = "1", Name="Created"},
                new PaymentStatus {Id = "2", Name="Success"},
                new PaymentStatus {Id = "3", Name="Failed"},
            };
        }

        public async Task<PaymentStatus> Add(PaymentStatus entity)
        {
            if (paymentStatuses.Count == 0)
            {
                entity.Id = 1.ToString();
            }
            else
            {
                entity.Id = (int.Parse(paymentStatuses.Last().Id) + 1).ToString();
            }
            paymentStatuses.Add(entity);
            return entity;
        }

        public async Task Delete(string id)
        {
            PaymentStatus? entity = await Get(id);
            if (entity != null)
            {
                paymentStatuses.Remove(entity);
            }
        }

        public async Task<PaymentStatus?> Get(string id)
        {
            return paymentStatuses.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<PaymentStatus>> GetAll()
        {
            return paymentStatuses;
        }

        public async Task<PaymentStatus?> Update(PaymentStatus entity)
        {
            PaymentStatus? original = await Get(entity.Id);
            if (original != null)
            {
                original.Name = entity.Name;
            }
            return original;
        }
    }
}
