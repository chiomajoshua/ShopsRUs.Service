using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Service.Features.Security.Services;

namespace ShopsRUs.Service.Features.Security
{
    public class ConfigureSecurityModule
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
        }
    }
}