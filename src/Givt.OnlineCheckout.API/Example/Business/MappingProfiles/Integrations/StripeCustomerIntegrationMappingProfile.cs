using AutoMapper;
using Givt.OnlineCheckout.API.Example.Integration.SDK;

namespace Givt.OnlineCheckout.API.Example.Business.MappingProfiles.Integrations
{
    public class StripeCustomerIntegrationMappingProfile : Profile
    {
        public StripeCustomerIntegrationMappingProfile()
        {
            // Van Business -> Integration
            CreateMap<CreateCustomerCommand, CustomerCreateOptions>();
            CreateMap<UpdateCustomerCommand, CustomerUpdateOptions>();
            // Van Integration -> Business
            CreateMap<Customer, CustomerDetailModel>();
        }
    }
}