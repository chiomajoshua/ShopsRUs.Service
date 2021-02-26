using System;
using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Service.Core.Storage.Models
{
    public class SalesHistory
    {
        [Key]
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string ItemsPurchased { get; set; }
        public double AmountPayed { get; set; }
        public DateTime DatePurchased { get; set; }
    }
}