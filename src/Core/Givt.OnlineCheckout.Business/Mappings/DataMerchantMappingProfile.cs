using AutoMapper;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.API.Mappings;
public class DataMerchantMappingProfile : Profile
{
    public DataMerchantMappingProfile()
    {
        // Domain -> Business
        CreateMap<MerchantData, MerchantDetailModel>();
    }
}