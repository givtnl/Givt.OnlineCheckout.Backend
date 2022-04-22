using AutoMapper;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.Organisations;
using System.Globalization;

namespace Givt.OnlineCheckout.API.Mappings;

public class OrganisationMappingProfile : Profile
{
    public OrganisationMappingProfile()
    {
        // Application -> Business
        CreateMap<ListOrganisationsRequest, ListOrganisationsQuery>();
        CreateMap<CreateOrganisationRequest, CreateOrganisationQuery>();
        CreateMap<GetOrganisationRequest, GetOrganisationQuery>();
        CreateMap<UpdateOrganisationRequest, UpdateOrganisationQuery>();

        CreateMap<ListOrganisationTextsRequest, ListOrganisationTextsQuery>();

        CreateMap<ListOrganisationMediumsRequest, ListOrganisationMediumsQuery>();

        CreateMap<ListOrganisationMediumTextsRequest, ListOrganisationMediumTextsQuery>();


        // Business -> Application
        CreateMap<OrganisationModel, OrganisationResponse>();

        CreateMap<LocalisableTextModel, LocalisableTextResponse>();
        CreateMap<MediumDetailModel, MediumResponse>();
    }

}