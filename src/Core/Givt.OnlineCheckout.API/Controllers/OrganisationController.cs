using AutoMapper;
using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.API.Models.Organisations.Create;
using Givt.OnlineCheckout.API.Models.Organisations.Get;
using Givt.OnlineCheckout.API.Models.Organisations.List;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Create;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Get;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.List;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Create;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Delete;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Get;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.List;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Update;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Update;
using Givt.OnlineCheckout.API.Models.Organisations.Texts.Create;
using Givt.OnlineCheckout.API.Models.Organisations.Texts.Get;
using Givt.OnlineCheckout.API.Models.Organisations.Texts.List;
using Givt.OnlineCheckout.API.Models.Organisations.Texts.Update;
using Givt.OnlineCheckout.API.Models.Organisations.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Delete;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Delete;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    // TODO: enable authentication/authorisation by uncommenting the line below    
    //[Authorize(Policy = "Retool")]
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
        /// <param name="request">Filter and pagination</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A page of data on organisations</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not found</response>
        [HttpGet()]
        [ProducesResponseType(typeof(ListOrganisationResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ListOrganisations(
            [FromQuery] ListOrganisationRequest request,
            CancellationToken cancellationToken)
        {
            var query = _mapper.Map<ListOrganisationsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<ListOrganisationResponse>(model);
            return Ok(response);
        }

        /// <summary>
        /// Create a new organisation
        /// </summary>
        /// <param name="request">Organisation information</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The updated organisation data</returns>
        /// <response code="201">Created</response>
        [HttpPost()]
        [ProducesResponseType(typeof(CreateOrganisationResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> CreateOrganisation(
            [FromQuery] CreateOrganisationRequest request,
            CancellationToken cancellationToken)
        {
            var query = _mapper.Map<CreateOrganisationCommand>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<CreateOrganisationResponse>(model);
            return CreatedAtRoute(
                nameof(ReadOrganisation),
                new
                {
                    organisationId = response.OrganisationId
                },
                response);
        }

        /// <summary>
        /// Read organisation
        /// </summary>
        /// <param name="organisationId">Organisation ID</param>
        /// <param name="cancellationToken"></param>        
        /// <returns>The organisation data</returns>
        [HttpGet("{organisationId:long}", Name = nameof(ReadOrganisation))]
        [ProducesResponseType(typeof(GetOrganisationResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ReadOrganisation(
            [FromRoute] long organisationId,
            CancellationToken cancellationToken)
        {
            var request = new GetOrganisationRequest
            {
                OrganisationId = organisationId
            };
            var query = _mapper.Map<ReadOrganisationQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<GetOrganisationResponse>(model);
            return Ok(response);
        }

        /// <summary>
        /// Update organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="request">Organisation data</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The updated organisation data</returns>
        /// <response code="200">OK</response>
        /// <response code="409">Concurrent update conflict</response>
        [HttpPut("{organisationId:long}")]
        [ProducesResponseType(typeof(UpdateOrganisationResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> UpdateOrganisation(
            [FromRoute] long organisationId,
            [FromBody] UpdateOrganisationRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateOrganisationCommand
            {
                OrganisationId = organisationId
            };
            _mapper.Map(request, command);
            var model = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<UpdateOrganisationResponse>(model);
            return Ok(response);
        }

        //  Delete is not (yet) needed

        #endregion

        #region Organisation->Text

        /// <summary>
        /// Get all localised texts for an organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The texts registered to this organisation</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not found</response>
        [HttpGet("{organisationId:long}/Text")]
        [ProducesResponseType(typeof(ListOrganisationTextsResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ListOrganisationTexts(
            [FromRoute] long organisationId,
            CancellationToken cancellationToken)
        {
            var request = new ListOrganisationTextsRequest
            {
                OrganisationId = organisationId
            };
            var query = _mapper.Map<ListOrganisationTextsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<ListOrganisationTextsResponse>(model);
            return Ok(response);
        }

        /// <summary>
        /// Create texts in a certain language for an organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="languageId">Language Identifier</param>
        /// <param name="request">The texts (core data)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Representation of the stored data</returns>
        [HttpPost("{organisationId:long}/Text/{languageId}")]
        [ProducesResponseType(typeof(CreateOrganisationTextsResponse), StatusCodes.Status201Created, "application/json")]
        public async Task<IActionResult> CreateOrganisationTexts(
            [FromRoute] long organisationId,
            [FromRoute] string languageId,
            [FromBody] CreateOrganisationTextsRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateOrganisationTextsCommand
            {
                OrganisationId = organisationId,
                LanguageId = languageId,
            };
            _mapper.Map(request, command);
            var model = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CreateOrganisationTextsResponse>(model);
            return CreatedAtRoute(
                nameof(ReadOrganisationText),
                new
                {
                    organisationId,
                    languageId = response.LanguageId
                },
                response);
        }

        /// <summary>
        /// Get texts in a certain language for an organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="languageId">Language Identifier for these texts, e.g. en-GB</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Representation of the stored data</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not found</response>
        [HttpGet("{organisationId:long}/Text/{languageId}", Name = nameof(ReadOrganisationText))]
        [ProducesResponseType(typeof(GetOrganisationTextsResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ReadOrganisationText(
            [FromRoute] long organisationId,
            [FromRoute] string languageId,
            CancellationToken cancellationToken)
        {
            var request = new GetOrganisationTextsRequest
            {
                OrganisationId = organisationId,
                LanguageId = languageId
            };
            var query = _mapper.Map<ReadOrganisationTextsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<GetOrganisationTextsResponse>(model);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// Update texts in a certain language for an organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="languageId">Language Identifier for these texts, e.g. en-GB</param>
        /// <param name="request">The texts</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="409">Concurrent update conflict</response>
        [HttpPut("{organisationId:long}/Text/{languageId}")]
        [ProducesResponseType(typeof(UpdateOrganisationTextsResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> UpdateOrganisationTexts(
            [FromRoute] long organisationId,
            [FromRoute] string languageId,
            [FromBody] UpdateOrganisationTextsRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateOrganisationTextsCommand
            {
                OrganisationId = organisationId,
                LanguageId = languageId
            };
            _mapper.Map(request, command);
            var model = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<UpdateOrganisationTextsResponse>(model);

            return Ok(response);
        }

        /// <summary>
        /// Delete texts in a certain language for an organisation
        /// </summary>
        /// <param name="organisationId">ID of the organisation</param>
        /// <param name="languageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{organisationId:long}/Text/{languageId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK, "application/json")]
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
        /// <param name="organisationId">ID of the organisation</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:long}/Medium")]
        [ProducesResponseType(typeof(ListOrganisationMediumsResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ListMediums(
            [FromRoute] long organisationId,
            CancellationToken cancellationToken)
        {
            var request = new ListOrganisationMediumsRequest
            {
                OrganisationId = organisationId
            };
            var query = _mapper.Map<ListOrganisationMediumsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<ListOrganisationMediumsResponse>(model); // TODO: Remove organisationID from results
            return Ok(response);
        }

        /// <summary>
        /// Create a medium for an organisation
        /// </summary>
        /// <param name="organisationId">ID of the organisation</param>
        /// <param name="request">Data for the medium</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{organisationId:long}/Medium")]
        [ProducesResponseType(typeof(CreateOrganisationMediumResponse), StatusCodes.Status201Created, "application/json")]
        public async Task<IActionResult> CreateMedium(
            [FromRoute] long organisationId,
            [FromBody] CreateOrganisationMediumRequest request,
            CancellationToken cancellationToken)
        {
            var query = new CreateOrganisationMediumCommand
            {
                OrganisationId = organisationId
            };
            _mapper.Map(request, query);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<CreateOrganisationMediumResponse>(model);
            return CreatedAtRoute(
                nameof(ReadMedium),
                new
                {
                    organisationId = response.OrganisationId,
                    mediumId = response.Medium
                },
                response);
        }

        /// <summary>
        /// Get a specific medium for an organisation
        /// </summary>
        /// <param name="organisationId">ID of the organisation</param>
        /// <param name="mediumId">The ID or Code of the medium</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:long}/Medium/{mediumId}", Name = nameof(ReadMedium))]
        [ProducesResponseType(typeof(GetOrganisationMediumResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ReadMedium(
            [FromRoute] long organisationId,
            [FromRoute] string mediumId,
            CancellationToken cancellationToken)
        {
            var request = new GetOrganisationMediumRequest
            {
                OrganisationId = organisationId,
                MediumId = mediumId
            };
            var query = _mapper.Map<ReadOrganisationMediumQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<GetOrganisationMediumResponse>(model);
            return Ok(response);
        }

        /// <summary>
        /// Update a specific medium for an organisation
        /// </summary>
        /// <param name="organisationId">ID of the organisation</param>
        /// <param name="mediumId">The ID or Code of the medium</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{organisationId:long}/Medium/{mediumId}")]
        [ProducesResponseType(typeof(UpdateOrganisationMediumResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> UpdateMedium(
            [FromRoute] long organisationId,
            [FromRoute] string mediumId,
            [FromBody] UpdateOrganisationMediumRequest request,
            CancellationToken cancellationToken)
        {
            var query = new UpdateOrganisationMediumCommand
            {
                OrganisationId = organisationId,
                Medium = mediumId // TODO: un-duplicate this (also part of request)
            };
            _mapper.Map(request, query);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<UpdateOrganisationMediumResponse>(model);
            return Ok(response);
        }

        //  Delete is not (yet) needed
        #endregion

        #region Organisation->Medium->Text
        /// <summary>
        /// Get all localised texts for a medium of an organisation
        /// </summary>
        /// <returns></returns>
        [HttpGet("{organisationId:long}/Medium/{mediumId}/Text")]
        [ProducesResponseType(typeof(ListOrganisationMediumTextsResponse), StatusCodes.Status200OK, "application/json")]
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
            var response = _mapper.Map<ListOrganisationMediumTextsResponse>(model);
            return Ok(response);
        }

        /// <summary>
        /// Create Texts in a certain language for a medium of an organisation
        /// </summary>
        /// <param name="organisationId">Organisation Identifier</param>
        /// <param name="mediumId">Medium Identifier (ID or code)</param>
        /// <param name="languageId">Language Identifier</param>
        /// <param name="request">The texts (core data)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{organisationId:long}/Medium/{mediumId}/Text/{languageId}")]
        [ProducesResponseType(typeof(CreateOrganisationMediumTextsResponse), StatusCodes.Status201Created, "application/json")]
        public async Task<IActionResult> CreateMediumText(
            [FromRoute] long organisationId,
            [FromRoute] string mediumId,
            [FromRoute] string languageId,
            [FromBody] CreateOrganisationMediumTextsRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateOrganisationMediumTextsCommand
            {
                OrganisationId = organisationId,
                MediumId = mediumId,
                LanguageId = languageId
            };
            _mapper.Map(request, command);
            var model = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CreateOrganisationMediumTextsResponse>(model);
            return CreatedAtRoute(
                nameof(ReadMediumText),
                new
                {
                    organisationId = response.OrganisationId,
                    mediumId = response.MediumId,
                    languageId = response.LanguageId
                },
                response);
        }

        /// <summary>
        /// Get texts in a certain language for a medium of an organisation
        /// </summary>
        /// <param name="organisationId">ID of the organisation</param>
        /// <param name="mediumId">The ID or Code of the medium</param>
        /// <param name="languageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:long}/Medium/{mediumId}/Text/{languageId}", Name = nameof(ReadMediumText))]
        [ProducesResponseType(typeof(GetOrganisationMediumTextsResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ReadMediumText(
            [FromRoute] long organisationId,
            [FromRoute] string mediumId,
            [FromRoute] string languageId,
            CancellationToken cancellationToken)
        {
            var request = new GetOrganisationMediumTextsRequest
            {
                OrganisationId = organisationId,
                MediumId = mediumId
            };
            var query = _mapper.Map<ReadOrganisationMediumTextsQuery>(request);
            var model = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<GetOrganisationMediumTextsResponse>(model);
            return Ok(response);
        }

        /// <summary>
        /// Update texts in a certain language for a medium of an organisation
        /// </summary>
        /// <param name="organisationId">ID of the organisation</param>
        /// <param name="mediumId">The ID or Code of the medium</param>
        /// <param name="languageId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{organisationId:long}/Medium/{mediumId}/Text/{languageId}")]
        [ProducesResponseType(typeof(UpdateOrganisationMediumTextsRequest), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> UpdateMediumTexts(
            [FromRoute] long organisationId,
            [FromRoute] string mediumId,
            [FromRoute] string languageId,
            [FromBody] UpdateOrganisationMediumTextsRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateOrganisationMediumTextsCommand
            {
                OrganisationId = organisationId,
                MediumId = mediumId, // TODO: un-duplicate this (also part of request)
                LanguageId = languageId,
            };
            _mapper.Map(request, command);
            var model = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<UpdateOrganisationMediumTextsResponse>(model);
            return Ok(response);
        }

        /// <summary>
        /// Delete texts in a certain language for a medium of an organisation
        /// </summary>
        /// <param name="organisationId">ID of the organisation</param>
        /// <param name="mediumId">The ID or Code of the medium</param>
        /// <param name="languageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{organisationId:long}/Medium/{mediumId}/Text/{languageId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> DeleteMediumTexts(
            [FromRoute] long organisationId,
            [FromRoute] string mediumId,
            [FromRoute] string languageId,
            CancellationToken cancellationToken)
        {
            var request = new DeleteOrganisationMediumTextsRequest
            {
                OrganisationId = organisationId,
                MediumId = mediumId,
                LanguageId = languageId
            };
            var command = _mapper.Map<DeleteOrganisationMediumTextsCommand>(request);
            var success = await _mediator.Send(command, cancellationToken);
            if (success)
                return Ok();
            return NotFound();
        }

        #endregion

    }
}
