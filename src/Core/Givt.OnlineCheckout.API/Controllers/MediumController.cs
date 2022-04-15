using AutoMapper;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.Mediums.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    public class MediumController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MediumController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Check whether a medium exists in the database
        /// </summary>
        /// <param name="request">Request json</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Exists</returns>
        /// <response code="200">found</response>
        /// <response code="400">malformed data</response>
        /// <response code="404">not found</response>
        [HttpHead]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckForExistence([FromQuery] GetMediumRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _mapper.Map<CheckMediumQuery>(request);
                var model = await _mediator.Send(query, cancellationToken);
                if (model)
                    return Ok();
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get information on a medium
        /// </summary>
        /// <param name="request">Request json</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Information on the medium</returns>
        /// <response code="200">information on the medium</response>
        /// <response code="400">malformed data</response>
        /// <response code="404">not found</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetMediumResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index([FromQuery] GetMediumRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Locale = LanguageUtils.GetLanguageId(request.Locale, HttpContext.Request.Headers.AcceptLanguage, "en");

                var query = _mapper.Map<GetMediumDetailsQuery>(request);
                var model = await _mediator.Send(query, cancellationToken);
                var response = _mapper.Map<GetMediumResponse>(model);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}