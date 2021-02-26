using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using ShopsRUs.Service.Core.Config;
using ShopsRUs.Service.Core.Identity;
using ShopsRUs.Service.Core.Mapping;
using ShopsRUs.Service.Core.Storage;
using ShopsRUs.Service.Features.Customer;
using ShopsRUs.Service.Features.Discounts;
using ShopsRUs.Service.Features.Invoice;
using ShopsRUs.Service.Features.Security;
using System;
using System.Collections.Generic;

namespace ShopsRUs.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDbContextModule.ConfigureServices(services, Configuration);

            var jwtTokenSettingsSection = Configuration.GetSection("JwtToken");
            services.Configure<JwtToken>(jwtTokenSettingsSection);
            var jwtTokenSettings = jwtTokenSettingsSection.Get<JwtToken>();
            ConfigureIdentityModule.ConfigureServices(services, jwtTokenSettings);

            ConfigureMappingModule.ConfigureServices(services);
            ConfigureSecurityModule.ConfigureServices(services);
            ConfigureDiscountsModule.ConfigureServices(services);
            ConfigureUserModule.ConfigureServices(services);
            ConfigureInvoiceModule.ConfigureServices(services);

            services.AddHealthChecks();
            services.AddLogging()
                    .AddCors();
            services.Configure<JwtToken>(Configuration.GetSection("JwtToken"));
            services.AddControllers();            
            ConfigureOpenApi(services);
        }

        public void ConfigureOpenApi(IServiceCollection services)
        {
            services.AddSwaggerDocument(config =>
            {
                // configure swagger properties
                config.PostProcess = document =>
                {
                    document.Info.Version = "V0.1";
                    document.Info.Description = "ShopsRUs  API";
                    document.Info.Title = "ShopsRUs";
                    document.Tags = new List<OpenApiTag>()
                    {
                        new OpenApiTag() {
                            Name = "ShopsRUs API",
                            Description = "API"
                        }
                    };
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUi3();
            }

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                 new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                 {
                     NoStore = true,
                     NoCache = true,
                     MustRevalidate = true,
                     MaxAge = TimeSpan.FromSeconds(0),
                     Private = true,
                 };
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("Pragma", "no-cache");
                await next();
            });

            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseRedirectValidation(t => t.AllowSameHostRedirectsToHttps(44361));
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseCsp(opts => opts
               .BlockAllMixedContent()
               .ScriptSources(s => s.Self())
               .ScriptSources(s => s.UnsafeEval())
               .ScriptSources(s => s.UnsafeInline())
               .StyleSources(s => s.UnsafeInline())
               .StyleSources(s => s.Self()));

            app.UseRouting();

            app.UseOpenApi();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // registration of health endpoints see https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
