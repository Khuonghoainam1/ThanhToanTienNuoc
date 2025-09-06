using System.ComponentModel.DataAnnotations;

namespace WaterService.Models
{
    public class MeterReading
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Reading Date")]
        [DataType(DataType.Date)]
        public DateTime ReadingDate { get; set; }

        [Required]
        [Display(Name = "Previous Reading")]
        public decimal PreviousReading { get; set; }

        [Required]
        [Display(Name = "Current Reading")]
        public decimal CurrentReading { get; set; }

        [Display(Name = "Consumption")]
        public decimal Consumption => CurrentReading - PreviousReading;

        [Display(Name = "Rate per Unit")]
        [DataType(DataType.Currency)]
        public decimal RatePerUnit { get; set; }

        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount => Consumption * RatePerUnit;

        [Display(Name = "Reading Type")]
        public ReadingType Type { get; set; } = ReadingType.Regular;

        [StringLength(500)]
        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
    }

    public enum ReadingType
    {
        Regular,
        Estimated,
        Final
    }
}