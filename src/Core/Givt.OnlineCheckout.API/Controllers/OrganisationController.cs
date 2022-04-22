using AutoMapper;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.Business.Organisations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    // TODO: enable authentication/authorisation by uncommenting the line below
    //[Authorize(Roles = "Givt Operator")] 
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
        /// <returns>A page of organisations</returns>
        /// <response code="200">organisations</response>
        /// <response code="400">malformed data</response>
        /// <response code="404">not found</response>
        [HttpGet()]
        [ProducesResponseType(typeof(List<OrganisationInfo>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ListOrganisations([FromQuery] ListOrganisationsRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<ListOrganisationsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<List<OrganisationInfo>>(model);
            return Ok(response);
        }

        /// <summary>
        /// Create a new organisation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The updated organisation data</returns>
        [HttpPost()]
        [ProducesResponseType(typeof(OrganisationInfo), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> CreateOrganisation([FromQuery] OrganisationInfoBase request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<CreateOrganisationQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<OrganisationInfo>(model);
            return Ok(response);
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{organisationId}")]
        public async Task<IActionResult> ReadOrganisation([FromRoute] GetOrganisationRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetOrganisationQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<OrganisationInfo>(model);
            return Ok(response);
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{organisationId}")]
        public async Task<IActionResult> UpdateOrganisation([FromRoute] int organisationId, [FromBody] UpdateOrganisationRequest request, CancellationToken cancellationToken)
        {
            if (organisationId != request.OrganisationId)
                return BadRequest("Organisation ID mismatch");
            var query = _mapper.Map<UpdateOrganisationQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<OrganisationInfo>(model);
            return Ok(response);
        }

        // TODO: decide if Delete is needed
        #endregion

        #region Organisation->Text

        /// <summary>
        /// Get all localised texts for an organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <returns></returns>
        [HttpGet("{organisationId}/Text")]
        public async Task<IActionResult> ListOrganisationTexts(int organisationId, CancellationToken cancellationToken)
        {
            var request = new ListOrganisationTextsRequest { OrganisationId = organisationId };
            var query = _mapper.Map<ListOrganisationTextsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<List<LocalisableTextInfo>>(model);
            return Ok(response);
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{organisationId}/Text/{languageId}")]
        public async Task<IActionResult> CreateOrganisationText(int organisationId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{organisationId}/Text/{languageId}")]
        public async Task<IActionResult> ReadOrganisationText(int organisationId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{organisationId}/Text/{languageId}")]
        public async Task<IActionResult> UpdateOrganisationTexts(int organisationId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("{organisationId}/Text/{languageId}")]
        public async Task<IActionResult> DeleteOrganisationTexts(int organisationId, string languageId)
        {
            // TODO: implement
            return Ok();
        }
        #endregion

        #region Organisation->Medium
        /// <summary>
        /// Get all mediums for an organisation
        /// </summary>
        /// <returns></returns>
        [HttpGet("{organisationId}/Medium")]
        public async Task<IActionResult> ListMediums([FromRoute] int organisationId, CancellationToken cancellationToken)
        {
            var request = new ListOrganisationMediumsRequest { OrganisationId = organisationId };
            var query = _mapper.Map<ListOrganisationMediumsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<List<MediumInfo>>(model);
            return Ok(response);
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{organisationId}/Medium")]
        public async Task<IActionResult> CreateMedium()
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{organisationId}/Medium/{mediumId}")]
        public async Task<IActionResult> ReadMedium(int organisationId, int mediumId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{organisationId}/Medium/{mediumId}")]
        public async Task<IActionResult> UpdateMedium(int organisationId, int mediumId)
        {
            // TODO: implement
            return Ok();
        }

        // TODO: decide if Delete is needed
        #endregion

        #region Organisation->Medium->Text
        /// <summary>
        /// Get all localised texts for a medium of an organisation
        /// </summary>
        /// <returns></returns>
        [HttpGet("{organisationId}/Medium/{mediumId}/Text")]
        public async Task<IActionResult> ListMediumTexts(int organisationId, string mediumId, CancellationToken cancellationToken)
        {
            var request = new ListOrganisationMediumTextsRequest
            {
                OrganisationId = organisationId,
                MediumId = mediumId
            };
            var query = _mapper.Map<ListOrganisationMediumTextsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<List<LocalisableTextInfo>>(model);
            return Ok(response);
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{organisationId}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> CreateOrganisationText(int organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{organisationId}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> ReadOrganisationText(int organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{organisationId}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> UpdateOrganisationTexts(int organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("{organisationId}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> DeleteOrganisationTexts(int organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }
        #endregion

    }
}
