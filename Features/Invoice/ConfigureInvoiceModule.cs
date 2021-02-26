using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Service.Features.Invoice.Services;

namespace ShopsRUs.Service.Features.Invoice
{
    public class ConfigureInvoiceModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IInvoiceService, InvoiceService>();
        }
    }
}