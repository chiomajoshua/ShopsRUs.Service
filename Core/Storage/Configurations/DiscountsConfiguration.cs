using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Service.Core.Storage.Models;
using System;

namespace ShopsRUs.Service.Core.Storage.Configurations
{
    public class DiscountsConfiguration : IEntityTypeConfiguration<Discounts>
    {
        public void Configure(EntityTypeBuilder<Discounts> builder)
        {
            builder.HasData(
                new Discounts
                {
                     DiscountPercentage = 10,
                     DiscountType = Enums.CustomerType.Affiliate.ToString(),
                     Id = Guid.NewGuid(),
                     Description = "10% discount for all store affiliates"
                },
                new Discounts
                {
                    DiscountPercentage = 5,
                    DiscountType = Enums.CustomerType.Customer.ToString(),
                    Id = Guid.NewGuid(),
                    Description = "5% discount for all customers above 2 years"
                },
                new Discounts
                {
                    DiscountPercentage = 30,
                    DiscountType = Enums.CustomerType.Employee.ToString(),
                    Id = Guid.NewGuid(),
                    Description = "30% discount for all Employees"
                });            
        }
    }
}
