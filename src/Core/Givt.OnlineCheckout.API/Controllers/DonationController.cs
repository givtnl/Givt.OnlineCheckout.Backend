using AutoMapper;
using Givt.OnlineCheckout.API.Models.Donations;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.QR.ApplicationFee.Get;
using Givt.OnlineCheckout.Business.QR.Donations.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers;

[Route("api/[controller]")]
public class DonationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly JwtTokenHandler _jwtTokenHandler;
    public DonationController(IMediator mediator, IMapper mapper, JwtTokenHandler jwtTokenHandler)
    {
        _mediator = mediator;
        _mapper = mapper;
        _jwtTokenHandler = jwtTokenHandler;
    }

    /// <summary>
    /// Create a Donation and Payment Intent
    /// </summary>
    /// <param name="request">Request json</param>
    /// <returns>Information about the newly created payment intent</returns>
    /// <response code="200">
    /// Stripe Client secret to complete the donation, and a token to fetch or send a report later.
    /// 
    ///     {
    ///         "paymentMethodId" : "string",
    ///         "token" : "string"
    ///     }
    /// </response>
    [HttpPost("intent")]
    [ProducesResponseType(typeof(CreateDonationIntentResponse), StatusCodes.Status200OK, "application/json")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreateDonationIntentRequest request)
    {
        request.Language = LanguageUtils.GetLanguageId(request.Language, HttpContext.Request.Headers.AcceptLanguage, "en");

        var applicationFee = await _mediator.Send(new GetApplicationFeeQuery { MediumIdType = MediumIdType.FromString(request.Medium) });
        
        var command = _mapper.MergeInto<CreateDonationIntentCommand>(request, applicationFee);

        var model = await _mediator.Send(command);
        var response = _mapper.Map<CreateDonationIntentResponse>(model, opt => { opt.Items[Keys.TOKEN_HANDLER] = _jwtTokenHandler; });
        return Ok(response);
    }

}