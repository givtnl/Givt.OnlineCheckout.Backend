using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Medium;
using Givt.OnlineCheckout.API.Mediums.Queries;
using Givt.OnlineCheckout.API.Models;

namespace Givt.OnlineCheckout.API.Mappings;

public class MediumMappingProfile: Profile
{
    public MediumMappingProfile()
    {
        CreateMap<GetMediumRequest, GetMediumDetailsQuery>();
        CreateMap<MediumDetailModel, GetMediumResponse>();
    }
}