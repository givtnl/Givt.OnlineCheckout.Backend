using System.Globalization;
using AutoMapper;
using Givt.OnlineCheckout.API.Models.Reports;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.QR.Reports.Get;
using Givt.OnlineCheckout.Business.QR.Reports.Send;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.API.Controllers;

[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILog _log;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly JwtTokenHandler _jwtTokenHandler;

    public ReportController(ILog log, IMapper mapper, IMediator mediator,
        JwtTokenHandler jwtTokenHandler)
    {
        _log = log;
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
        _log.Debug("Get Report/singleDonation {0}", new object[]{request});
        try
        {
            var _ = _jwtTokenHandler.GetTransactionReference(HttpContext.User);
        }
        catch (UnauthorizedAccessException uae)
        {
            return Unauthorized(uae.Message);
        }

        request.Language = LanguageUtils.GetLanguageId(request.Language, HttpContext.Request.Headers.AcceptLanguage, "en");

        var query = _mapper.Map<GetDonationReportCommand>(request, opt =>
        {
            opt.Items[Keys.TOKEN_HANDLER] = _jwtTokenHandler;
            opt.Items[Keys.USER] = HttpContext.User;
            opt.Items[Keys.CULTURE] = new CultureInfo(request.Language);
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
        _log.Debug("Post Report/singleDonation {0}", new object[] {request});

        request.Language = LanguageUtils.GetLanguageId(request.Language, HttpContext.Request.Headers.AcceptLanguage, "en");        

        try
        {
            var notification = _mapper.Map<SendDonationReportNotification>(request, opt =>
            {
                opt.Items[Keys.TOKEN_HANDLER] = _jwtTokenHandler;
                opt.Items[Keys.USER] = HttpContext.User;
                opt.Items[Keys.CULTURE] = new CultureInfo(request.Language); ;
            });
            await _mediator.Publish(notification, CancellationToken.None); // decouple from HTTP server cancellations etc.
            return Ok();
        }
        catch (UnauthorizedAccessException uae)
        {
            return Unauthorized(uae.Message);
        }
    }

    //private CultureInfo FindCulture(string language, HttpContext httpContext)
    //{
    //    CultureInfo culture = null;
    //    if (!String.IsNullOrWhiteSpace(language))
    //    {
    //        try { culture = CultureInfo.GetCultureInfo(language); }
    //        catch (CultureNotFoundException) { }
    //    }

    //    if (culture == null)
    //    {
    //        var requestCultureFeature = Request.HttpContext.Features.Get<IRequestCultureFeature>();
    //        culture = requestCultureFeature?.RequestCulture.Culture; // This usually defaults to 'en'
    //    }

    //    if (culture == null || culture == CultureInfo.InvariantCulture)
    //        culture = new CultureInfo("en-GB");
    //    return culture;
    //}
}
