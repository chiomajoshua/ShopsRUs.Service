using ShopsRUs.Service.Features.Discounts.Models;
using System;

namespace ShopsRUs.Service.Features.Discounts
{
    public static class DiscountExtensions
    {
        public static Discount ToDiscount(this Core.Storage.Models.Discounts discounts)
        {
            return new Discount
            {
                DiscountPercentage = discounts.DiscountPercentage,
                DiscountType = discounts.DiscountType,
                Description = discounts.Description
            };
        }

        public static Core.Storage.Models.Discounts ToDbDiscount(this CreateDiscountTypeRequest discount)
        {
            return new Core.Storage.Models.Discounts
            {
                Id = Guid.NewGuid(),
                DiscountPercentage = discount.Deduction,
                DiscountType = discount.DiscountType,
                Description = discount.Description
            };
        }
    }
}