using System;
using System.Collections.Generic;

namespace ShopsRUs.Service.Features.Customer.Models
{
    public class User
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Blocked { get; set; }
        public string UserId { get; set; }
        public string Designation { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public DateTime DateRegistered { get; set; }

        public override string ToString()
        {
            return $"{Firstname} {Lastname} ({UserName})";
        }
    }
}