using Givt.OnlineCheckout.API.Models.Errors;
using Givt.OnlineCheckout.Business.Validations;
using Microsoft.AspNetCore.Mvc;
using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ValidateController : ControllerBase
    {
        private readonly ILog _logger;

        public ValidateController(ILog logger)
        {
            _logger = logger;
        }

        [HttpGet("Email")]
        public async Task<IActionResult> Email([FromQuery] string email, CancellationToken cancellationToken)
        {
            var message = await EmailAddress.IsValid(email, _logger, cancellationToken);

            if (message == null)
            {
                _logger.Debug($"Request to validate email '{email}' -> OK");
                return Ok(email);
            }
            else
            {

                _logger.Debug($"Request to validate email '{email}' -> {message}");
                return BadRequest(new ErrorResponse
                {
                    Message = message,
                    Name = nameof(email),
                    Value = email
                });
            }
        }
    }
}
