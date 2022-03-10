using AutoMapper;
using Givt.OnlineCheckout.Application.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Application.Mappings;
public class DataMerchantMappingProfile : Profile
{
    public DataMerchantMappingProfile()
    {
        // Domain -> Business
        CreateMap<DataMerchant, MerchantDetailModel>();
    }
}