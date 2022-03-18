using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Merchants;
using Givt.OnlineCheckout.API.Merchants.Queries;
using Givt.OnlineCheckout.API.Models;

namespace Givt.OnlineCheckout.API.Mappings
{
    public class MerchantMappingProfile : Profile
    {
        public MerchantMappingProfile()
        {
            // Application -> Business
            CreateMap<GetMerchantRequest, GetMerchantByMediumIdQuery>();
            // Business -> Application
            CreateMap<MerchantDetailModel, GetMerchantResponse>();
        }
    }
}
