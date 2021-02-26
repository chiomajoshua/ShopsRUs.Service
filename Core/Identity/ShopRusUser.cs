using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Service.Core.Storage.Models
{
    public class ShopRusUser : IdentityUser
    {
        [Required]
        public string UserId { get; set; }
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool IsAdmin { get; set; }


        [Required]
        [DefaultValue(true)]
        public bool Enabled { get; set; }
        [Required]
        public string Designation { get; set; }
        public virtual ICollection<ShopRusUserRole> Roles { get; set; } = new List<ShopRusUserRole>();

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({UserName})";
        }
        public DateTime DateEmployed { get; set; }
    }

    public class ShopRusUserRole : IdentityUserRole<string>
    {

        public virtual ShopRusUser Employee { get; set; }

        public virtual ShopRusRole Role { get; set; }

    }

    public class ShopRusRole : IdentityRole
    {

        public virtual ICollection<ShopRusUserRole> UserRoles { get; set; } = new List<ShopRusUserRole>();
    }
}