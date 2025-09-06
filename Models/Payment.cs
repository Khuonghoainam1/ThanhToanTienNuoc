using System.ComponentModel.DataAnnotations;

namespace WaterService.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Invoice")]
        public int InvoiceId { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Payment Method")]
        public PaymentMethod Method { get; set; }

        [StringLength(100)]
        [Display(Name = "Reference Number")]
        public string? ReferenceNumber { get; set; }

        [StringLength(500)]
        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Invoice Invoice { get; set; } = null!;
    }

    public enum PaymentMethod
    {
        Cash,
        BankTransfer,
        CreditCard,
        Check,
        Online
    }
}