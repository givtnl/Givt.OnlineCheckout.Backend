using AutoMapper;
using Givt.OnlineCheckout.API.Example.Business;
using Givt.OnlineCheckout.API.Example.Business;

namespace Givt.OnlineCheckout.API.Example.Application.MappingProfiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        // Application -> Business
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
        CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>();

        // Application <- Business
        CreateMap<CustomerDetailModel, CreateCustomerResponse>();
        CreateMap<CustomerDetailModel, UpdateCustomerResponse>();
    }
}
