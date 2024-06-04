using Kolejki.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolejki.ApplicationCore.Services
{
    public interface IPaymentService
    {
        Task<Payment> CreatePayment(string buyerEmail, string paypalId, int amount);
        Task<Payment> ChangePaymentStatusByPaypalId(string paypalId, string status);
        Task<Payment> ChangePaymentStatus(string paymentId, string status);
    }
}
