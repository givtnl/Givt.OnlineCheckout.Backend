using AutoMapper;
using Givt.OnlineCheckout.API.Models.Donations;
using Givt.OnlineCheckout.Business.Donations;

namespace Givt.OnlineCheckout.API.Mappings;

public class DonationMappingProfile : Profile
{
    public DonationMappingProfile()
    {
        CreateMap<CreateDonationIntentRequest, CreateDonationIntentCommand>()
            .ForMember(x => x.MediumId, options => options.MapFrom(
            src => src.Medium
        ));
    }
}