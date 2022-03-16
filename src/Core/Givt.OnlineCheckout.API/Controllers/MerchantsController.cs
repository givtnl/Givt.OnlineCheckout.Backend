using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Merchants;
using Givt.OnlineCheckout.Application.Merchants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    public class MerchantsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MerchantsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetMerchantRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetMerchantByMediumIdQuery>(request);
            return Ok(await _mediator.Send(query, cancellationToken));
        }
    }
}