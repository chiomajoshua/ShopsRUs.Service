using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Service.Core.Storage.Models;
using System;

namespace ShopsRUs.Service.Core.Storage.Configurations
{
    public class ShopRusUserConfiguration : IEntityTypeConfiguration<ShopRusUser>
    {
        public void Configure(EntityTypeBuilder<ShopRusUser> builder)
        {
            var randomDigit = new Random();
            builder.HasData(
            new ShopRusUser
            {
                Address = "40 St Gregory College Road, Ibahams",
                DateRegistered = DateTime.Now.AddYears(-3),
                Designation = Enums.CustomerType.Customer.ToString(),
                Email = "JaneDoe@gmail.com",
                Enabled = true,
                FirstName = "Jane",
                Gender = "Female",
                LastName = "Doe",
                PhoneNumber = "+23470623459087",
                Title = "Miss",
                UserName = "JaneDoe@gmail.com",
                UserId = $"CUST-{randomDigit.Next(100, 99999)}"
            },
            new ShopRusUser
            {
                Address = "1104 Onion Boulevard,Shabams",
                DateRegistered = DateTime.Now,
                DateEmployed = DateTime.Now,
                Designation = Enums.CustomerType.Employee.ToString(),
                Email = "JohnPeterson@shopsrus.co",
                Enabled = true,
                FirstName = "John",
                Gender = "Male",
                LastName = "Peterson",
                PhoneNumber = "+23470531059087",
                Title = "Mr",
                UserName = "JohnPeterson@shopsrus.co",
                IsAdmin = false,
                UserId = $"SHPRUSTF-{randomDigit.Next(100, 99999)}"
            },
            new ShopRusUser
            {
                Address = "113 Vanilla Crescent, IJGB",
                DateRegistered = DateTime.Now,
                Designation = Enums.CustomerType.Affiliate.ToString(),
                Email = "KayoWalker@hotmail.com",
                Enabled = true,
                FirstName = "Kayo",
                Gender = "Male",
                LastName = "Walker",
                PhoneNumber = "+23470531059087",
                Title = "Mr",
                UserName = "KayoWalker@hotmail.com",
                IsAdmin = false,
                UserId = $"CUST-{randomDigit.Next(100, 99999)}"
            });
        }
    }
}