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
        
        // This head method is used to check whether a medium exists in our databse
        [HttpHead]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckForExistence([FromQuery] GetMediumRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<CheckMediumQuery>(request);
            if(await _mediator.Send(query, cancellationToken))
                return Ok();
            
            return NotFound();
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(GetMediumResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index([FromQuery] GetMediumRequest request, CancellationToken cancellationToken)
        {
            request.Locale = LocaleUtils.GetLocaleId(request.Locale, HttpContext.Request.Headers.AcceptLanguage, "en");

            var query = _mapper.Map<GetMediumDetailsQuery>(request);
            return Ok(await _mediator.Send(query, cancellationToken));   
        }
    }
}