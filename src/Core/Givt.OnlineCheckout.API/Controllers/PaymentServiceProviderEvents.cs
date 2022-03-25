using AutoMapper;
using Givt.OnlineCheckout.API.Models.PaymentProvider;
using Givt.OnlineCheckout.Business.PaymentProviders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.API.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)] // do not show in Swagger to protect it from abuse
    [Route("/psp")]
    //[ApiController]
    public class PaymentServiceProviderEvents : ControllerBase
    {
        private readonly ILog _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PaymentServiceProviderEvents(ILog logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("stripe")]
        public async Task<IActionResult> WebHook(CancellationToken cancellationToken)
        {
            _logger.Debug("Stripe Webhook called");

            // Stripe sends JSON as the body, cannot directly let ASP.Net Core map that into a class
            // future: Other PSPs might do the same
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var eventData = new PaymentProviderEvent
            {
                Content = json,
                MetaData = HttpContext.Request.Headers
            };
            // Note: we could just as well populate the object on the business layer directly, but we love structures and mappers
            var command = _mapper.Map<PaymentProviderEventData>(eventData); 
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
