using System.ComponentModel.DataAnnotations;

namespace Kolejki.API.Data.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PaypalId { get; set; } = null!;

        [Required]
        public int Amount { get; set; }

        [Required]
        public string BuyerEmail { get; set; } = null!;
    }
}
