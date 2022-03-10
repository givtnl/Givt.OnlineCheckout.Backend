using AutoMapper;
using Givt.OnlineCheckout.API.Example.Application;
using Givt.OnlineCheckout.API.Example.Application.MappingProfiles;
using Givt.OnlineCheckout.API.Example.Business;
using Givt.OnlineCheckout.API.Example.Business.MappingProfiles.Integrations;
using Givt.OnlineCheckout.API.Example.Integration;
using Givt.OnlineCheckout.API.Example.Integration.SDK;
using Givt.OnlineCheckout.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
            new StripeCustomerIntegrationMappingProfile()
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

app.MapPost("api/customers", async ([FromBody] CreateCustomerRequest applicationRequest, [FromServices] IMediator mediator, IMapper mapper) =>
{
    var businessRequest = mapper.Map<CreateCustomerCommand>(applicationRequest);
    var businessResponse = await mediator.Send(businessRequest);
    return new CreatedResult("api/customers", mapper.Map<CreateCustomerResponse>(businessResponse));
});

app.MapPut("api/customers", async ([FromBody] UpdateCustomerRequest applicationRequest, [FromServices] IMediator mediator, IMapper mapper) =>
{
    var businessRequest = mapper.Map<UpdateCustomerCommand>(applicationRequest);
    var businessResponse = await mediator.Send(businessRequest);
    return new CreatedResult("api/customers", mapper.Map<UpdateCustomerResponse>(businessResponse));
});

app.Run();