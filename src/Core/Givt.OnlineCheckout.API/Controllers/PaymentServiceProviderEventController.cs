using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.Events;
using Givt.OnlineCheckout.Business.Events.PaymentServiceProvider;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)] // do not show in Swagger to protect it from abuse
    [Route("/psp")]
    //[ApiController]
    public class PaymentServiceProviderEventController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly IMediator _mediator;

        public PaymentServiceProviderEventController(ILog logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("stripe")]
        public async Task<IActionResult> StripeWebHook(CancellationToken cancellationToken)
        {
            // Stripe sends JSON as the body, cannot directly let ASP.Net Core map that into a string
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            
            _logger.Debug("Stripe Webhook called:\n{0}", new object[] { json });
            
            await _mediator.Send(new PaymentServiceProviderEventCommand(json, HttpContext.Request.Headers), cancellationToken);

            return Ok();
        }
    }

}
