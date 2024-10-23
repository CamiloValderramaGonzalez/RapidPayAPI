using System.ComponentModel.DataAnnotations;

namespace RapidPay.Domain.Entities
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Card number must be 15 digits.")]
        public string CardNumber { get; set; }

        [Required]
        public decimal Balance { get; set; } = 0.0m;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
