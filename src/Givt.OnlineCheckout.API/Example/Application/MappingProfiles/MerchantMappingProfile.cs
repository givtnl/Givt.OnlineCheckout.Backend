using AutoMapper;
using Givt.OnlineCheckout.API.Example.Business;

namespace Givt.OnlineCheckout.API.Example.Application.MappingProfiles;
public class MerchantMappingProfile : Profile
{
    public MerchantMappingProfile()
    {
        // Application -> Business
        CreateMap<GetMerchantRequest, GetMerchantByMediumIdQuery>();
        // Application <- Business
        CreateMap<MerchantDetailModel, GetMerchantResponse>();
    }
}