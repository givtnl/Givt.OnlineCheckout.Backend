using AutoMapper;
using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.Organisations;

namespace Givt.OnlineCheckout.API.Mappings
{
    public class OrganisationMappingProfile : Profile
    {
        public OrganisationMappingProfile()
        {
            // Application -> Business
            CreateMap<ListOrganisationsRequest, ListOrganisationsQuery>();
            CreateMap<CreateOrganisationRequest, CreateOrganisationQuery>();
            CreateMap<GetOrganisationRequest, GetOrganisationQuery>();
            CreateMap<UpdateOrganisationRequest, UpdateOrganisationQuery>();
            // Business -> Application
            CreateMap<OrganisationModel, OrganisationResponse>();
            CreateMap<OrganisationModel, CreateOrganisationResponse>();
            CreateMap<OrganisationModel, GetOrganisationResponse>();
            CreateMap<OrganisationModel, UpdateOrganisationResponse>();
        }
    }
}
