

using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Givt.OnlineCheckout.API.Mappings;
using Givt.OnlineCheckout.API.Requests.Customers;
using Givt.OnlineCheckout.API.Requests.Merchants;
using Givt.OnlineCheckout.Application.Customers.Commands;
using Givt.OnlineCheckout.Application.Mappings;
using Givt.OnlineCheckout.Application.Merchants.Queries;
using Givt.OnlineCheckout.Integrations.Stripe;
using Givt.OnlineCheckout.Integrations.Stripe.SDK;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration;

builder.Host.ConfigureServices(services =>
{
    //services.AddAzureAppConfiguration();

    // Example
    services.AddSingleton<CustomerService>();
    services.AddSingleton<StripeIntegration>();
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfiles(new List<Profile>
        {
            new CustomerMappingProfile(),
            new MerchantMappingProfile(),
            new DataCustomerMappingProfile(),
            new DataMerchantMappingProfile(),
            new StripeIntegrationMappingProfile()
        });
    });
    IMapper autoMapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(autoMapper);

    services.AddMediatR(typeof(GetMerchantByMediumIdQuery).Assembly);
    services.AddDbContext<OnlineCheckoutContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DataBaseConnectionString"));
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
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI((options) =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

});

app.MapGet("/api/organisations", async ([FromQuery] GetMerchantRequest query, [FromServices] IMediator mediator, IMapper mapper) =>
{
    var businessResponse = mapper.Map<GetMerchantResponse>(query);
    return await mediator.Send(query);
});

// Dien post iere ga ne customer aanmaken en datecreated + datemodified zetten via nen AuditableEntity en een hook in EF opt event Entity.Created
app.MapPost("api/customers", async ([FromBody] CreateCustomerRequest applicationRequest, [FromServices] IMediator mediator, IMapper mapper) =>
{
    var businessRequest = mapper.Map<CreateCustomerCommand>(applicationRequest);
    var businessResponse = await mediator.Send(businessRequest);
    return mapper.Map<CreateCustomerResponse>(businessResponse);
});

// Dien put gaat de datemodified zetten via tzelfste AuditableEntity model een hook in EF opt event Entity.Modified
app.MapPut("api/customers", async ([FromBody] UpdateCustomerRequest applicationRequest, [FromServices] IMediator mediator, IMapper mapper) =>
{
    var businessRequest = mapper.Map<UpdateCustomerCommand>(applicationRequest);
    var businessResponse = await mediator.Send(businessRequest);
    return mapper.Map<UpdateCustomerResponse>(businessResponse);
});

app.Run();