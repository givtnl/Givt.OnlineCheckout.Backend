using System.Globalization;
using AutoMapper;
using Givt.OnlineCheckout.Application.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Application.Mappings;

public class DataMediumMappingProfile: Profile
{
    public DataMediumMappingProfile()
    {
        CreateMap<DataMedium, MediumDetailModel>()
            .ForMember(
                x => x.Amounts,
                options => options.MapFrom(
                    src => src.Amounts.Split(',', StringSplitOptions.None).Select(str => decimal.Parse(str, CultureInfo.InvariantCulture)).ToList()
                    ))
            .ForMember(
                x => x.OrganisationName, 
                options => options.MapFrom(
                    src => src.Merchant.Name
                    ));
    }
}