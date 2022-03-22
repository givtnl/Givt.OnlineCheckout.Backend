using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Customers;
using Givt.OnlineCheckout.API.Customers.Commands;
using Givt.OnlineCheckout.API.Models;

namespace Givt.OnlineCheckout.API.Mappings
{
    public class CustomerMappingProfile : Profile { 
    
    public CustomerMappingProfile()
    {
        // Application -> Business
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
        CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>();

        // Business -> Application
        CreateMap<CustomerDetailModel, CreateCustomerResponse>();
        CreateMap<CustomerDetailModel, UpdateCustomerResponse>();
    }
    }
}
