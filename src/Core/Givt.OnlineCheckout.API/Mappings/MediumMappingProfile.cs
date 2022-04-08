using AutoMapper;
using Givt.OnlineCheckout.API.Extensions;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.Business.Mediums.Queries;
using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Mappings;

public class MediumMappingProfile : Profile
{
    public MediumMappingProfile()
    {
        CreateMap<GetMediumRequest, GetMediumDetailsQuery>()
            .ForMember(dst => dst.Language, options => options.MapFrom(src => src.Locale));
        CreateMap<GetMediumRequest, CheckMediumQuery>();
        CreateMap<MediumDetailModel, GetMediumResponse>()
            .ForMember(dst => dst.PaymentMethods, options => options.MapFrom(
                (src) => src.MapPaymentMethods()
            ));
    }
}