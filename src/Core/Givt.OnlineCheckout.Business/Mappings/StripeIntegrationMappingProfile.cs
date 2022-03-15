using AutoMapper;
using Givt.OnlineCheckout.Application.Customers.Commands;
using Givt.OnlineCheckout.Application.Models;
using Givt.OnlineCheckout.Integrations.Stripe.SDK;

namespace Givt.OnlineCheckout.Application.Mappings
{
    public class StripeIntegrationMappingProfile : Profile
    {
        public StripeIntegrationMappingProfile()
        {
            // Van Business -> Integration
            CreateMap<CreateCustomerCommand, CustomerCreateOptions>();
            CreateMap<UpdateCustomerCommand, CustomerUpdateOptions>();
            // Van Integration -> Business
            CreateMap<Customer, CustomerDetailModel>();
        }
    }
}