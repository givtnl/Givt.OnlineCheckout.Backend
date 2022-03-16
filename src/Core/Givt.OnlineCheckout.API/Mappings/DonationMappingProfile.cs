using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Donations;
using Givt.OnlineCheckout.Application.Donations;

namespace Givt.OnlineCheckout.API.Mappings;

public class DonationMappingProfile : Profile
{
    public DonationMappingProfile()
    {
        CreateMap<CreateDonationIntentRequest, CreateDonationIntentCommand>();
    }
}