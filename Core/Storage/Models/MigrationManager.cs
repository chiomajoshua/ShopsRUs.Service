using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ShopsRUs.Service.Core.Storage.Models
{
    public static class MigrationManager
    {
        public static IWebHost MigrateDatabase(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var appContext = scope.ServiceProvider.GetRequiredService<ShopsRusDbContext>();
                try
                {
                    appContext.Database.Migrate();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return host;
        }
    }
}