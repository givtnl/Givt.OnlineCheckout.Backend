using AutoMapper;
using Givt.OnlineCheckout.API.Filters;
using Givt.OnlineCheckout.API.Mappings;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.Mappings;
using Givt.OnlineCheckout.Business.Organisations.Queries;
using Givt.OnlineCheckout.Infrastructure.Behaviors;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Infrastructure.Loggers;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Postmark;
using Givt.OnlineCheckout.Integrations.Stripe;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog.Sinks.Http.Logger;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Givt.OnlineCheckout.Integrations.GoogleDocs;
using ReportMappingProfile = Givt.OnlineCheckout.API.Mappings.ReportMappingProfile;

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

            var log = new LogitHttpLogger(Configuration["LogitConfiguration:Tag"], Configuration["LogitConfiguration:Key"]);
            services.AddSingleton<ILog, LogitHttpLogger>(x => log);
            services.AddSingleton(log.SerilogLogger);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfiles(new List<Profile>
                {
                    new DonationMappingProfile(),
                    new DonorMappingProfile(),
                    new MediumMappingProfile(),
                    new OrganisationMappingProfile(),
                    new ReportMappingProfile(),

                    new DataDonorMappingProfile(),
                    new DataMediumMappingProfile(),
                    new DataOrganisationMappingProfile(),
                    new DonationReportMappingProfile(),
                });
            }).CreateMapper());

            services.AddSingleton<ISinglePaymentService, StripeIntegration>();

            var jwtSection = Configuration.GetSection(JwtOptions.SectionName);
            services.Configure<JwtOptions>(jwtSection)
                .AddSingleton(sp => sp.GetRequiredService<IOptions<JwtOptions>>().Value);

            services.Configure<StripeOptions>(Configuration.GetSection(StripeOptions.SectionName))
                .AddSingleton(sp => sp.GetRequiredService<IOptions<StripeOptions>>().Value);

            services.Configure<PostmarkOptions>(Configuration.GetSection(PostmarkOptions.SectionName))
                .AddSingleton(sp => sp.GetRequiredService<IOptions<PostmarkOptions>>().Value);

            services.Configure<GoogleDocsOptions>(Configuration.GetSection(GoogleDocsOptions.SectionName))
                .AddSingleton(sp => sp.GetRequiredService<IOptions<GoogleDocsOptions>>().Value);

            services.AddMediatR(
                typeof(GetOrganisationByMediumIdQuery).Assembly,            // Givt.OnlineCheckout.Business
                typeof(ISinglePaymentNotification).Assembly,                // Givt.OnlineCheckout.Integrations.Interfaces
                typeof(StripeIntegration).Assembly,                         // Givt.OnlineCheckout.Integrations.Stripe
                typeof(PostmarkEmailService<IEmailNotification>).Assembly   // Givt.OnlineCheckout.Integrations.Postmark
            );

            services.AddTransient(typeof(JwtTokenHandler));
            services.AddTransient<IPdfService, GooglePdfService>();

            var jwtOptions = jwtSection.Get<JwtOptions>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey));
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience, // ???
                        IssuerSigningKey = key,
                        ClockSkew = TimeSpan.FromMinutes(1),
                    };
                });


            services.AddDbContext<OnlineCheckoutContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("GivtOnlineCheckoutDbDebug"));
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

            app.UseAuthentication(); // To support JWT Bearer tokens

            app.UseCors("EnableAll")
                .UseMvc();
        }

        public void ConfigureOptions(IServiceCollection services)
        {
            services.AddAzureAppConfiguration();
        }
    }
}