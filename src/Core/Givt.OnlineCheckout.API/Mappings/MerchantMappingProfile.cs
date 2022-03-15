using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Merchants;
using Givt.OnlineCheckout.Application.Merchants.Queries;
using Givt.OnlineCheckout.Application.Models;

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
