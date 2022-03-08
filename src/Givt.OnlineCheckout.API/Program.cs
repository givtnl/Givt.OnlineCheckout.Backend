using Givt.OnlineCheckout.API.Requests.Queries.GetOrganisationDetailsFromMedium;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices(services =>
{
    services.AddMediatR(typeof(GetOrganisationDetailsFromMediumQuery));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((options) =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Givt Online Checkout API",
        Description = "The API microservice to support online giving without the Givt app and without registering within the app."
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

app.MapGet("/api/organisations", async ([FromBody] GetOrganisationDetailsFromMediumQuery query, [FromServices] IMediator mediator) =>
{
    return await mediator.Send(query);
});

app.Run();