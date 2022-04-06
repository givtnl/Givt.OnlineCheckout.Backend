using AutoMapper;
using Givt.OnlineCheckout.API.Models.Reports;
using Givt.OnlineCheckout.API.Utils;
using MediatR;
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

    [HttpGet("singleDonation")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK, "application/octet-stream")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get([FromQuery] GetDonationReportRequest request,
        CancellationToken cancellationToken)
    {
        _logger.Debug("Get Report/singleDonation {0}", request);

        if (String.IsNullOrWhiteSpace(request.Locale))
            request.Locale = HttpContext.Request.Headers.AcceptLanguage.FirstOrDefault() ?? "en";
        try
        {
            request.TransactionReference = _jwtTokenHandler.GetTransactionReference(HttpContext);

            var query = _mapper.Map<GetDonationReportRequest>(request);
            var response = (GetDonationReportResponse)await _mediator.Send(query, cancellationToken);
            return File(response.Content, response.MimeType, response.Filename);
        }
        catch (UnauthorizedAccessException uae)
        {
            return Unauthorized(uae.Message);
        }
    }

    [HttpPost("singleDonation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Send([FromQuery] SendDonationReportRequest request)
    {
        _logger.Debug("Post Report/singleDonation {0}", request);

        if (String.IsNullOrWhiteSpace(request.Locale))
            request.Locale = HttpContext.Request.Headers.AcceptLanguage.FirstOrDefault() ?? "en";
        try
        {
            request.TransactionReference = _jwtTokenHandler.GetTransactionReference(HttpContext);

            var notification = _mapper.Map<SendDonationReportRequest>(request);
            await _mediator.Publish(notification, CancellationToken.None);
            return Ok();
        }
        catch (UnauthorizedAccessException uae)
        {
            return Unauthorized(uae.Message);
        }
    }

}
