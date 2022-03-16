using AutoMapper;
using Givt.OnlineCheckout.Application.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Application.Mappings;

public class DataMediumMappingProfile: Profile
{
    public DataMediumMappingProfile()
    {
        // mapping voor amounts en voor orgname
        CreateMap<DataMedium, MediumDetailModel>();
        // merchant name in mediumdetail
        // amounts string split op komma en naar decimal array
    }
}