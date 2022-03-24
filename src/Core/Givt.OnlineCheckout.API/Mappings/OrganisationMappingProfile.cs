using AutoMapper;
using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.Organisations.Queries;

namespace Givt.OnlineCheckout.API.Mappings
{
    public class OrganisationMappingProfile : Profile
    {
        public OrganisationMappingProfile()
        {
            // Application -> Business
            CreateMap<GetOrganisationRequest, GetOrganisationByMediumIdQuery>();
            // Business -> Application
            CreateMap<OrganisationDetailModel, GetOrganisationResponse>();
        }
    }
}
