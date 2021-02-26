using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Service.Features.Customer.Services;

namespace ShopsRUs.Service.Features.Customer
{
    public class ConfigureUserModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}