using AutoMapper;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.Business.Mediums.Queries;
using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Mappings;

public class MediumMappingProfile: Profile
{
    public MediumMappingProfile()
    {
        CreateMap<GetMediumRequest, GetMediumDetailsQuery>();
        CreateMap<GetMediumRequest, CheckMediumQuery>();
        CreateMap<MediumDetailModel, GetMediumResponse>();
    }
}