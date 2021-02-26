using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Service.Features.Customer.Models
{
    public class CreateUserRequest
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Password { get; set; }
        [Required]
        public string Title { get; set; }
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Designation { get; set; }
        public bool IsAdmin { get; set; }
    }
}