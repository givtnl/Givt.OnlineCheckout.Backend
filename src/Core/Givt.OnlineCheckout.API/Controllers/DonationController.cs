using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Donations;
using Givt.OnlineCheckout.Application.Donations;
using Givt.OnlineCheckout.Application.Exceptions;
using Givt.OnlineCheckout.Integrations.Stripe;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers;

public class DonationController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DonationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("api/[controller]/intent")]
    [ProducesResponseType(typeof(CreateDonationIntentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreateDonationIntentRequest request)
    {
        try
        {
            var command = _mapper.Map<CreateDonationIntentCommand>(request);
            var response = await _mediator.Send(command);
            return Ok(new CreateDonationIntentResponse { PaymentMethodId = response.PaymentIntentSecret });
        }
        catch (NotFoundException exception)
        {
            return NotFound();
        }
        catch (BadRequestException exception)
        {
            return BadRequest();
        }
    }
}