using System;
using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Service.Core.Storage.Models
{
    public class Products
    {
        [Key]
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string ProductType { get; set; }
        public string Quantity { get; set; }
        public string BatchCode { get; set; }
        public string SerialNumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime DateAdded { get; set; }
    }
}