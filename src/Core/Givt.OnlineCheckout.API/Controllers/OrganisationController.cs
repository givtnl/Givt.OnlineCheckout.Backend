using AutoMapper;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.API.Models.Organisations.Get;
using Givt.OnlineCheckout.API.Models.Organisations.List;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.QR.Organisations.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Delete;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    // TODO: enable authentication/authorisation by uncommenting the line below
    //[Authorize(Roles = "Site Admin,Givt Operator")] 
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
        /// <returns>A page of data on organisations</returns>
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
        /// <response code="201">organisation</response>
        [HttpPost()]
        [ProducesResponseType(typeof(OrganisationInfo), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> CreateOrganisation([FromQuery] OrganisationInfoBase request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<CreateOrganisationQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<OrganisationInfo>(model);
            return CreatedAtRoute(nameof(ReadOrganisation), new { organisationId = response.Id }, response);
        }

        /// <summary>
        /// Read organisation
        /// </summary>
        /// <param name="organisationId">Organisation ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:long}", Name = nameof(ReadOrganisation))]
        public async Task<IActionResult> ReadOrganisation([FromRoute] long organisationId, CancellationToken cancellationToken)
        {
            var request = new GetOrganisationRequest { OrganisationId = organisationId };
            var query = _mapper.Map<ReadOrganisationQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<OrganisationInfo>(model);
            return Ok(response);
        }

        /// <summary>
        /// Update organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{organisationId:long}")]
        public async Task<IActionResult> UpdateOrganisation(
            [FromRoute] long organisationId, 
            [FromBody] UpdateOrganisationRequest request, CancellationToken cancellationToken)
        {
            if (organisationId != request.Id)
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
        /// <param name="cancellationToken"></param>
        /// <returns>The texts registered to this organisation</returns>
        [HttpGet("{organisationId:long}/Text")]
        public async Task<IActionResult> ListOrganisationTexts(long organisationId, CancellationToken cancellationToken)
        {
            var request = new ListOrganisationTextsRequest { OrganisationId = organisationId };
            var query = _mapper.Map<ListOrganisationTextsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<List<LocalisableTextsResponse>>(model);
            return Ok(response);
        }

        /// <summary>
        /// Create Texts for an organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="languageId">Language Identifier</param>
        /// <param name="request">The texts (core data)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Representation of the stored data</returns>
        [HttpPost("{organisationId:long}/Text/{languageId}")]
        public async Task<IActionResult> CreateOrganisationText(
            [FromRoute] long organisationId,
            [FromRoute] string languageId,
            [FromBody] CreateOrganisationTextsRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateOrganisationTextsCommand>(request, opt =>
            {
                opt.Items[Keys.ORGANISATION_ID] = organisationId;
                opt.Items[Keys.LANGUAGE_ID] = languageId;
            });
            var model = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CreateOrganisationTextsResponse>(model);

            return CreatedAtRoute(nameof(ReadOrganisationText), new { organisationId, languageId }, response);
        }

        /// <summary>
        /// Get texts in a certain language for an organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="languageId">Language Identifier for these texts, e.g. en-GB</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:long}/Text/{languageId}", Name = nameof(ReadOrganisationText))]
        public async Task<IActionResult> ReadOrganisationText(
            [FromRoute] long organisationId,
            [FromRoute] string languageId,
            CancellationToken cancellationToken)
        {
            var query = new ReadOrganisationTextsQuery
            {
                OrganisationId = organisationId,
                LanguageId = languageId
            };
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<GetOrganisationTextsResponse>(model);
            return Ok(response);
        }

        /// <summary>
        /// Not yet implenmented - Update texts in a certain language for an organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="languageId">Language Identifier for these texts, e.g. en-GB</param>
        /// <param name="request">The texts</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{organisationId:long}/Text/{languageId}")]
        public async Task<IActionResult> UpdateOrganisationTexts(
            [FromRoute] long organisationId,
            [FromRoute] string languageId,
            [FromBody] CreateOrganisationTextsRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateOrganisationTextsCommand>(request, opt =>
            {
                opt.Items[Keys.ORGANISATION_ID] = organisationId;
                opt.Items[Keys.LANGUAGE_ID] = languageId;
            });
            var model = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<UpdateOrganisationTextsResponse>(model);

            return Ok(response);
        }

        /// <summary>
        /// Not yet implenmented - Delete texts in a certain language for an organisation
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="languageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{organisationId:long}/Text/{languageId}")]
        public async Task<IActionResult> DeleteOrganisationTexts(
            [FromRoute] long organisationId,
            [FromRoute] string languageId,
            CancellationToken cancellationToken)
        {
            var command = new DeleteOrganisationTextsCommand
            {
                OrganisationId = organisationId,
                LanguageId = languageId
            };
            var success = await _mediator.Send(command, cancellationToken);
            if (success)
                return Ok();
            return NotFound();
        }
        #endregion

        #region Organisation->Medium
        /// <summary>
        /// Get all mediums for an organisation
        /// </summary>
        /// <returns></returns>
        [HttpGet("{organisationId:long}/Medium")]
        public async Task<IActionResult> ListMediums([FromRoute] long organisationId, CancellationToken cancellationToken)
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
        [HttpPost("{organisationId:long}/Medium")]
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
        [HttpGet("{organisationId:long}/Medium/{mediumId}")]
        public async Task<IActionResult> ReadMedium(long organisationId, int mediumId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{organisationId:long}/Medium/{mediumId}")]
        public async Task<IActionResult> UpdateMedium(long organisationId, int mediumId)
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
        [HttpGet("{organisationId:long}/Medium/{mediumId}/Text")]
        public async Task<IActionResult> ListMediumTexts(
            [FromRoute] long organisationId,
            [FromRoute] string mediumId,
            CancellationToken cancellationToken)
        {
            var request = new ListOrganisationMediumTextsRequest
            {
                OrganisationId = organisationId,
                MediumId = mediumId
            };
            var query = _mapper.Map<ListOrganisationMediumTextsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<List<LocalisableTextsResponse>>(model);
            return Ok(response);
        }

        /// <summary>
        /// Create Texts for a medium
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="mediumId">Medium Identifier (ID or code)</param>
        /// <param name="languageId">Language Identifier</param>
        /// <param name="request">The texts (core data)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{organisationId:long}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> CreateMediumText(
            [FromRoute] long organisationId,
            [FromRoute] string mediumId,
            [FromRoute] string languageId,
            [FromBody] CreateOrganisationTextsRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateOrganisationMediumTextsCommand>(request, opt =>
            {
                opt.Items[Keys.ORGANISATION_ID] = organisationId;
                opt.Items[Keys.MEDIUM_ID] = mediumId;
                opt.Items[Keys.LANGUAGE_ID] = languageId;
            });
            var model = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CreateOrganisationMediumTextsResponse>(model);
            return Ok(response);
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:long}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> ReadOrganisationMediumText(long organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{organisationId:long}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> UpdateOrganisationMediumTexts(long organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("{organisationId:long}/Medium/{mediumId}/Text/{languageId}")]
        public async Task<IActionResult> DeleteOrganisationMediumTexts(long organisationId, int mediumId, string languageId)
        {
            // TODO: implement
            return Ok();
        }
        #endregion

    }
}
