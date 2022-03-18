using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Donations;
using Givt.OnlineCheckout.API.Donations;

namespace Givt.OnlineCheckout.API.Mappings;

public class DonationMappingProfile : Profile
{
    public DonationMappingProfile()
    {
        CreateMap<CreateDonationIntentRequest, CreateDonationIntentCommand>();
    }
}