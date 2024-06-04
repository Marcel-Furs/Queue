using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolejki.ApplicationCore.Models
{
    public class Payment
    {
        public string Id { get; set; } = null!;
        public string PaypalId { get; set; } = null!;
        public int Amount { get; set; }
        public string BuyerEmail { get; set; } = null!;
        public PaymentStatus PaymentStatus { get; set; } = null!;
    }
}
