using AutoMapper;
using Givt.OnlineCheckout.API.Customers.Commands;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.API.Mappings;

public class DataCustomerMappingProfile : Profile
{
    public DataCustomerMappingProfile()
    {
        // Domain -> Business
        CreateMap<DataCustomer, CustomerDetailModel>();
    }
}
