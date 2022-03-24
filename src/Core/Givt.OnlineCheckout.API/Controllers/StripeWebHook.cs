using AutoMapper;
using Givt.OnlineCheckout.API.Models.Errors;
using Givt.OnlineCheckout.API.Models.PaymentProvider;
using Givt.OnlineCheckout.Business.PaymentProviders;
using Givt.OnlineCheckout.Business.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class StripeWebHook : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILog _logger;

        public StripeWebHook(ILog logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            _logger.Debug("Stripe Webhook called");
                        
            var eventData = new PaymentProviderEvent { 
                Stream = HttpContext.Request.Body, 
                MetaData = HttpContext.Request.Headers };
            await _mediator.Send(eventData, cancellationToken);

            return Ok();
        }
    }
}
