using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Service.Features.Discounts.Models
{
    public class CreateDiscountTypeRequest
    {
        [Required]
        public double Deduction { get; set; }
        [Required]
        public string DiscountType { get; set; }
        [Required]
        public string Description { get; set; }
    }
}