using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Service.Features.Invoice.Models
{
    public class InvoiceRequest
    {
        [Required]
        public double Total { get; set; }
        [Required]
        public bool IsGrocery { get; set; }
        [Required]
        public string Email { get; set; }
    }
}