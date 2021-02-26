using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Service.Core.Storage.Models;
using System;

namespace ShopsRUs.Service.Core.Storage
{
    public static class ConfigureDbContextModule
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("PGSqlConnectionString");

            if (string.IsNullOrWhiteSpace(connString))
                throw new Exception("Please Set The Connection String");

            services.AddTransient<DbContext, ShopsRusDbContext>();

            services.AddDbContext<ShopsRusDbContext>(options => 
            options.UseNpgsql(connString,
            b => b.MigrationsAssembly(typeof(ShopsRusDbContext).Assembly.FullName)));

            services.AddIdentity<ShopRusUser, ShopRusRole>()
                .AddEntityFrameworkStores<ShopsRusDbContext>()
                .AddDefaultTokenProviders();

            
        }
    }
}