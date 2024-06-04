using Kolejki.ApplicationCore.Models;
using Kolejki.ApplicationCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolejki.ApplicationCore.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> paymentRepository;
        private readonly IRepository<PaymentStatus> paymentStatusRepository;

        public PaymentService(IRepository<Payment> paymentRepository, IRepository<PaymentStatus> paymentStatusRepository)
        {
            this.paymentRepository = paymentRepository;
            this.paymentStatusRepository = paymentStatusRepository;
        }

        public async Task<Payment> ChangePaymentStatus(string paymentId, string status)
        {
            PaymentStatus? paymentStatus = (await paymentStatusRepository.GetAll()).FirstOrDefault(x => x.Name == status) 
                ?? throw new ArgumentException("Invalid status");
            Payment? payment = (await paymentRepository.GetAll()).FirstOrDefault(x => x.Id == paymentId)
                ?? throw new ArgumentException("Invalid paymentId");
            payment.PaymentStatus = paymentStatus;
            await paymentRepository.Update(payment);
            return payment;
        }

        public async Task<Payment> ChangePaymentStatusByPaypalId(string paypalId, string status)
        {
            PaymentStatus? paymentStatus = (await paymentStatusRepository.GetAll()).FirstOrDefault(x => x.Name == status)
                ?? throw new ArgumentException("Invalid status");
            Payment? payment = (await paymentRepository.GetAll()).FirstOrDefault(x => x.PaypalId == paypalId)
                ?? throw new ArgumentException("Invalid paypalId");
            payment.PaymentStatus = paymentStatus;
            await paymentRepository.Update(payment);
            return payment;
        }

        public async Task<Payment> CreatePayment(string buyerEmail, string paypalId, int amount)
        {
            PaymentStatus? status = (await paymentStatusRepository.GetAll()).FirstOrDefault(x => x.Name == "Created");

            Payment payment = new Payment
            {
                Amount = amount,
                BuyerEmail = buyerEmail,
                PaypalId = paypalId,
                PaymentStatus = status
            };
            await paymentRepository.Add(payment);
            return payment;
        }
    }
}
