using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShopsRUs.Service.Core.Storage.Models;
using System.Threading.Tasks;

namespace ShopsRUs.Service.Features.Security.Services
{
    public interface IAuthService
    {
        Task<bool> ValidateCredentials(string userName, string password);
    }
    public class AuthService : IAuthService
    {

        private readonly UserManager<ShopRusUser> _userManager;
        private readonly ILogger<AuthService> _logger;
        public AuthService(UserManager<ShopRusUser> userManager, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var account = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.CheckPasswordAsync(account, password);
            if (result)
            {
                return true;
            }
            _logger.LogError($"ValidateCredentials: Login Not Successful for {account.Email}");
            return false;
        }
    }
}