using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Merchants;
using Givt.OnlineCheckout.Application.Mediums.Queries;
using Givt.OnlineCheckout.Application.Merchants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    public class MediumController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MediumController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(GetMediumResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index([FromQuery] GetMediumRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetMediumDetailsQuery>(request);
            return Ok(await _mediator.Send(query, cancellationToken));
        }
    }
}