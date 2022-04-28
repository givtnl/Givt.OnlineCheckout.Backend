using System.Globalization;
using AutoMapper;
using Givt.OnlineCheckout.API.Models.Reports;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.QR.Reports.Get;
using Givt.OnlineCheckout.Business.QR.Reports.Send;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers;

[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly JwtTokenHandler _jwtTokenHandler;

    public ReportController(Serilog.ILogger logger, IMapper mapper, IMediator mediator,
        JwtTokenHandler jwtTokenHandler)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
        _jwtTokenHandler = jwtTokenHandler;
    }

    /// <summary>
    /// Get donation report for a donation.
    /// Bearer token is needed for authentication and identification of the transaction.
    /// </summary>
    /// <param name="request">Request json</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Report file</returns>
    /// <response code="200">report</response>
    /// <response code="401">no valid transaction reference given</response>
    [HttpGet("singleDonation")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK, "application/octet-stream")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get([FromQuery] GetDonationReportRequest request,
        CancellationToken cancellationToken)
    {
        _logger.Debug("Get Report/singleDonation {0}", request);
        try
        {
            var _ = _jwtTokenHandler.GetTransactionReference(HttpContext.User);
        }
        catch (UnauthorizedAccessException uae)
        {
            return Unauthorized(uae.Message);
        }

        var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
        request.CurrentCulture = requestCulture?.RequestCulture.Culture; // This defaults to 'en' :) nice feature
            
        var query = _mapper.Map<GetDonationReportCommand>(request, opt =>
        {
            opt.Items[Keys.TOKEN_HANDLER] = _jwtTokenHandler;
            opt.Items[Keys.USER] = HttpContext.User;
        });
        var response = await _mediator.Send(query, cancellationToken);
        return File(response.Content, response.MimeType, response.Filename);
    }

    /// <summary>
    /// Sends a donation report for a donation to an email address. 
    /// Bearer token is needed for authentication and identification of the transaction.
    /// </summary>
    /// <param name="request">Request json</param>
    /// <response code="200">report is sent</response>
    /// <response code="401">no valid transaction reference given</response>

    [HttpPost("singleDonation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Send([FromQuery] SendDonationReportRequest request)
    {
        _logger.Debug("Post Report/singleDonation {0}", request);

        var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
        request.CurrentCulture = requestCulture?.RequestCulture.Culture; // This defaults to 'en' :) nice feature
        
        try
        {
            var notification = _mapper.Map<SendDonationReportNotification>(request, opt =>
            {
                opt.Items[Keys.TOKEN_HANDLER] = _jwtTokenHandler;
                opt.Items[Keys.USER] = HttpContext.User;
            });
            await _mediator.Publish(notification, CancellationToken.None); // decouple from HTTP server cancellations etc.
            return Ok();
        }
        catch (UnauthorizedAccessException uae)
        {
            return Unauthorized(uae.Message);
        }
    }

}
