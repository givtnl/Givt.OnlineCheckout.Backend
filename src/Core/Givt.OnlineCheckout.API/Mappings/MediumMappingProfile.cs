using AutoMapper;
using Givt.OnlineCheckout.API.Requests.Merchants;
using Givt.OnlineCheckout.Application.Mediums.Queries;
using Givt.OnlineCheckout.Application.Models;

namespace Givt.OnlineCheckout.API.Mappings;

public class MediumMappingProfile: Profile
{
    public MediumMappingProfile()
    {
        CreateMap<GetMediumRequest, GetMediumDetailsQuery>();
        CreateMap<MediumDetailModel, GetMediumResponse>();
    }
}