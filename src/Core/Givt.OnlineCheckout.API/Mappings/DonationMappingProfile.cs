using AutoMapper;
using Givt.OnlineCheckout.API.Models.Donations;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.QR.ApplicationFee.Get;
using Givt.OnlineCheckout.Business.QR.Donations.Create;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.API.Mappings;

public class DonationMappingProfile : Profile
{
    public DonationMappingProfile()
    {
        CreateMap<CreateDonationIntentRequest, CreateDonationIntentCommand>()
            .ForMember(dst => dst.MediumId,
                options => options.MapFrom(
                    src => MediumIdType.FromString(src.Medium)
            ))
            .ForMember(dst => dst.PaymentMethod,
                options => options.MapFrom(
                    src => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), src.PaymentMethod, true)
            ));

        CreateMap<GetApplicationFeeQueryResponse, CreateDonationIntentCommand>();
        
        CreateMap<CreateDonationIntentCommandResponse, CreateDonationIntentResponse>()
            .ForMember(x => x.PaymentMethodId,
                options => options.MapFrom(
                    src => src.PaymentIntentSecret
            ))
            .ForMember(x => x.Token,
                options => options.MapFrom(
                    (src, dst, _, context) => (context.Items[Keys.TOKEN_HANDLER] as JwtTokenHandler)?.GetBearerToken(src.TransactionReference)
            ));
    }
}