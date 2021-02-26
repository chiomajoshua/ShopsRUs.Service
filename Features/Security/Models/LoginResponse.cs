using ShopsRUs.Service.Core.Storage.Models;

namespace ShopsRUs.Service.Features.Security.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public ShopRusUser Account { get; set; }
    }
}