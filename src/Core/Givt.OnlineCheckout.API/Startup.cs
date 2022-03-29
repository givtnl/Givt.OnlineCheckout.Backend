using AutoMapper;
using Givt.OnlineCheckout.API.Filters;
using Givt.OnlineCheckout.API.Mappings;
using Givt.OnlineCheckout.Business.Mappings;
using Givt.OnlineCheckout.Business.Organisations.Queries;
using Givt.OnlineCheckout.Infrastructure.Behaviors;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Infrastructure.Loggers;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Integrations.Stripe;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog.Sinks.Http.Logger;
using System.Reflection;

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

            services.AddSingleton<ILog, LogitHttpLogger>(x => new LogitHttpLogger(Configuration["LogitConfiguration:Tag"], Configuration["LogitConfiguration:Key"]));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfiles(new List<Profile>
                {
                    new DonorMappingProfile(),
                    new OrganisationMappingProfile(),
                    new DataDonorMappingProfile(),
                    new DataOrganisationMappingProfile(),
                    new MediumMappingProfile(),
                    new DataMediumMappingProfile(),
                    new DonationMappingProfile()
                });
            }).CreateMapper());

            services.AddSingleton<ISinglePaymentService, StripeIntegration>();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"))
                .AddSingleton(sp => sp.GetRequiredService<IOptions<StripeSettings>>().Value);

            services.AddMediatR(
                typeof(GetOrganisationByMediumIdQuery).Assembly,    // Givt.OnlineCheckout.Business
                typeof(ISinglePaymentNotification).Assembly,         // Givt.OnlineCheckout.Integrations.Interfaces
                typeof(StripeIntegration).Assembly                  // Givt.OnlineCheckout.Integrations.Stripe
            );

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
            services.AddControllers();
            services.AddMvcCore(x =>
            {
                x.Filters.Add<CustomExceptionFilter>();
            })
                    .AddControllersAsServices()
                    .AddMvcOptions(o => o.EnableEndpointRouting = false)
                    .AddCors(o => o.AddPolicy("EnableAll", builder =>
                    {
                        builder.AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin();
                    }));
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

            app.UseCors("EnableAll")
                .UseMvc();
        }

        public void ConfigureOptions(IServiceCollection services)
        {
            services.AddAzureAppConfiguration();
        }
    }
}