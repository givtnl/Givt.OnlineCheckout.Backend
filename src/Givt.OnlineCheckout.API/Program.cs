using Givt.OnlineCheckout.API.Requests.Queries.GetOrganisationDetailsFromMedium;
using Givt.OnlineCheckout.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration;

builder.Host.ConfigureServices(services =>
{
    services.AddAzureAppConfiguration();
    services.AddMediatR(typeof(GetOrganisationDetailsFromMediumQuery));
    services.AddDbContext<OnlineCheckoutContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DataBaseConnectionString"));
    });
    services.AddAutoMapper(typeof(Program));
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

app.MapGet("/api/organisations", async ([FromQuery] GetOrganisationDetailsFromMediumQuery query, [FromServices] IMediator mediator) =>
{
    return await mediator.Send(query);
});

app.Run();