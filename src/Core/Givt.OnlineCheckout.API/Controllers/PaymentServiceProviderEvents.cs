using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)] // do not show in Swagger to protect it from abuse
    [Route("/psp")]
    //[ApiController]
    public class PaymentServiceProviderEvents : ControllerBase
    {
        class RawSinglePaymentNotification : IRawSinglePaymentNotification
        {
            public string RawData { get; set; }
            public IDictionary<string, StringValues> MetaData { get; set; }
        }


        private readonly ILog _logger;
        private readonly IMediator _mediator;

        public PaymentServiceProviderEvents(ILog logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("stripe")]
        public async Task<IActionResult> StripeWebHook(CancellationToken cancellationToken)
        {
            _logger.Debug("Stripe Webhook called");

            // Stripe sends JSON as the body, cannot directly let ASP.Net Core map that into a string
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            // create a notification record and send it to implementor(s)
            var notification = new RawSinglePaymentNotification
            {
                RawData = json,
                MetaData = HttpContext.Request.Headers
            };

            await _mediator.Publish(notification, CancellationToken.None); // decouple from HTTP server cancellations etc.

            return Ok();
        }
    }

}
