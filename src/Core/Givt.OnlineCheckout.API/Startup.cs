using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Givt.OnlineCheckout.API.Mappings;
using Givt.OnlineCheckout.Application.Mappings;
using Givt.OnlineCheckout.Application.Merchants.Queries;
using Givt.OnlineCheckout.Integrations.Stripe;
using Givt.OnlineCheckout.Integrations.Stripe.SDK;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Givt.OnlineCheckout.API
{
    public class Startup
    {
        public IHostEnvironment HostEnvironment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureOptions(services);
            
            services.AddSingleton<CustomerService>();
            services.AddSingleton<StripeIntegration>();
            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfiles(new List<Profile>
                {
                    new CustomerMappingProfile(),
                    new MerchantMappingProfile(),
                    new DataCustomerMappingProfile(),
                    new DataMerchantMappingProfile(),
                    new StripeIntegrationMappingProfile()
                });
            }).CreateMapper());

            services.AddMediatR(typeof(GetMerchantByMediumIdQuery).Assembly);
            services.AddDbContext<OnlineCheckoutContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DataBaseConnectionString"));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen((options) =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Givt Online Checkout API",
                    Description = "The API microservice to support online giving without the Givt app and without registering within the app."
                });
            });
            services.AddMvc();
            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            Console.WriteLine($"Givt.OnlineCheckout.API started on {env.EnvironmentName}");
            
            
            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI((options) =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

            });
        }

        public void ConfigureOptions(IServiceCollection services)
        {
            services.AddAzureAppConfiguration();
        }
    }
}