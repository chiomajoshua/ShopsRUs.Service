using System;
using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Service.Core.Storage.Models
{
    public class Discounts
    {
        [Key]
        public Guid Id { get; set; }
        public double DiscountPercentage { get; set; }
        public string DiscountType { get; set; }
        public string Description { get; set; }
    }
}