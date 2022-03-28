using AutoMapper;
using Givt.OnlineCheckout.API.Models.Donations;
using Givt.OnlineCheckout.Business.Donations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers;

public class DonationController : ControllerBase
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
        var command = _mapper.Map<CreateDonationIntentCommand>(request);
        var response = await _mediator.Send(command);
        return Ok(new CreateDonationIntentResponse { PaymentMethodId = response.PaymentIntentSecret });
    }
}