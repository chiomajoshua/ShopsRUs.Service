using Microsoft.Extensions.DependencyInjection;

namespace ShopsRUs.Service.Core.Mapping
{
    public class ConfigureMappingModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}