using AutoMapper;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.QR.Mediums.Check;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.API.Mappings;

public class MediumMappingProfile : Profile
{
    public MediumMappingProfile()
    {
        CreateMap<GetMediumRequest, GetMediumDetailsQuery>()
            .ForMember(dst => dst.Language, options => options.MapFrom(src => src.Locale))
            .ForMember(dst => dst.MediumId, options => options.MapFrom(src => MediumIdType.FromString(src.Code)));
        CreateMap<GetMediumRequest, CheckMediumQuery>()
            .ForMember(dst => dst.MediumId, options => options.MapFrom(src => MediumIdType.FromString(src.Code)));
        CreateMap<MediumFlattenedModel, GetMediumResponse>()
            .ForMember(dst => dst.PaymentMethods,
                options => options.MapFrom(src => GetPaymentMethodsAsString(src.PaymentMethods)));
    }

    // TODO: check if AutoMapper supports mapping from string <-> PaymentMethod, so we implement those and let AutoMapper do the work for list creation etc.
    private string[] GetPaymentMethodsAsString(IEnumerable<PaymentMethod> paymentMethods)
    {
        var result = new List<string>();
        foreach (var paymentMethod in paymentMethods)
        {
            result.Add(paymentMethod.ToString().ToLowerInvariant());
        }
        result.Sort();
        return result.ToArray();
    }
}