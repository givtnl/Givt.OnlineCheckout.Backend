using AutoMapper;
using Givt.OnlineCheckout.Application.Customers.Commands;
using Givt.OnlineCheckout.Application.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Application.Mappings;

public class DataCustomerMappingProfile : Profile
{
    public DataCustomerMappingProfile()
    {
        // Domain -> Business
        CreateMap<DataCustomer, CustomerDetailModel>();
    }
}
