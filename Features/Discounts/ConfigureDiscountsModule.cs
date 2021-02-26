using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Service.Features.Discounts.Services;

namespace ShopsRUs.Service.Features.Discounts
{
    public class ConfigureDiscountsModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDiscountService, DiscountService>();
        }
    }
}