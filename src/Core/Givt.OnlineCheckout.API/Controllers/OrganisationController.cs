using AutoMapper;
using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.Business.Organisations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "OrganisationAdmin")]
    public class OrganisationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrganisationController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        #region Organisation

        /// <summary>
        /// List Organisations
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <returns>A page of organisations</returns>
        /// <response code="200">organisations</response>
        /// <response code="400">malformed data</response>
        /// <response code="404">not found</response>
        [HttpGet()]
        [ProducesResponseType(typeof(List<OrganisationResponse>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ListOrganisations([FromQuery] ListOrganisationsRequest request, CancellationToken cancellationToken)
        {

            var query = _mapper.Map<ListOrganisationsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<List<OrganisationResponse>>(model);
            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateOrganisation([FromQuery] CreateOrganisationRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<CreateOrganisationQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<CreateOrganisationResponse>(model);
            return Ok(response);
        }

        [HttpGet("{organisationId}")]
        public async Task<IActionResult> ReadOrganisation([FromRoute] GetOrganisationRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetOrganisationQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<GetOrganisationResponse>(model);
            return Ok(response);
        }

        [HttpPut("{organisationId}")]
        public async Task<IActionResult> UpdateOrganisation([FromRoute] int organisationId, [FromBody] UpdateOrganisationRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<UpdateOrganisationQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<UpdateOrganisationResponse>(model);
            return Ok(response);
        }

        // TODO: decide if Delete is needed
        #endregion

        #region Organisation->Text
        [HttpGet("{organisationId}/Text")]
        public async Task<IActionResult> ListOrganisationTexts(int organisationId)
        {
            // TODO: implement
            return Ok();
        }

        [HttpPost("{organisationId}/Text/{languageId}")]
        public async Task<IActionResult> CreateOrganisationText(int organisationId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        [HttpGet("{organisationId}/Text/{languageId}")]
        public async Task<IActionResult> ReadOrganisationText(int organisationId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        [HttpPut("{organisationId}/Text/{languageId}")]
        public async Task<IActionResult> UpdateOrganisationTexts(int organisationId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        [HttpDelete("{organisationId}/Text/{languageId}")]
        public async Task<IActionResult> DeleteOrganisationTexts(int organisationId, string languageId)
        {
            // TODO: implement
            return Ok();
        }
        #endregion

        #region Organisation->Medium
        [HttpGet("{organisationId}/Medium")]
        public async Task<IActionResult> ListMediums()
        {
            // TODO: implement
            return Ok();
        }

        [HttpPost("{organisationId}/Medium")]
        public async Task<IActionResult> CreateMedium()
        {
            // TODO: implement
            return Ok();
        }

        [HttpGet("{organisationId}/Medium/{mediumId}")]
        public async Task<IActionResult> ReadMedium(int organisationId, int mediumId)
        {
            // TODO: implement
            return Ok();
        }

        [HttpPut("{organisationId}/Medium/{mediumId}")]
        public async Task<IActionResult> UpdateMedium(int organisationId, int mediumId)
        {
            // TODO: implement
            return Ok();
        }

        // TODO: decide if Delete is needed
        #endregion

        #region Organisation->Medium->Text
        [HttpGet("{organisationId}/Medium/{mediumId}/Text")]
        public async Task<IActionResult> ReadOrganisationTexts(int organisationId, int mediumId)
        {
            // TODO: implement
            return Ok();
        }

        [HttpPost("{organisationId}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> CreateOrganisationText(int organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        [HttpGet("{organisationId}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> ReadOrganisationText(int organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        [HttpPut("{organisationId}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> UpdateOrganisationTexts(int organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        [HttpDelete("{organisationId}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> DeleteOrganisationTexts(int organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }
        #endregion

    }
}
