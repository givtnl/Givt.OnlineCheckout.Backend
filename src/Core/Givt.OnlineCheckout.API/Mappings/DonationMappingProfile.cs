using AutoMapper;
using Givt.OnlineCheckout.API.Models.Donations;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.Donations;

namespace Givt.OnlineCheckout.API.Mappings;

public class DonationMappingProfile : Profile
{
    public DonationMappingProfile()
    {
        CreateMap<CreateDonationIntentRequest, CreateDonationIntentCommand>()
            .ForMember(x => x.MediumId,
                options => options.MapFrom(
                    src => src.Medium
            ));
        CreateMap<CreateDonationIntentCommandResponse, CreateDonationIntentResponse>()
            .ForMember(x => x.PaymentMethodId, 
                options => options.MapFrom(
                    src => src.PaymentIntentSecret
            ))
            .ForMember(x => x.Token,
                options => options.MapFrom(
                    (src, dst, _, context) => (context.Items["TokenHandler"] as JwtTokenHandler)?.GetBearerToken(src.TransactionReference)
            ));
    }
}