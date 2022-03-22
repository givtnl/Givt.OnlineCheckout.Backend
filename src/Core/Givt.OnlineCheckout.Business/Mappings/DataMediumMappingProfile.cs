using System.Globalization;
using AutoMapper;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.API.Mappings;

public class DataMediumMappingProfile: Profile
{
    public DataMediumMappingProfile()
    {
        CreateMap<MediumData, MediumDetailModel>()
            .ForMember(
                x => x.Amounts,
                options => options.MapFrom(
                    src => src.Amounts.Split(',', StringSplitOptions.None).Select(str => decimal.Parse(str, CultureInfo.InvariantCulture)).ToList()
                    ))
            .ForMember(
                x => x.OrganisationName, 
                options => options.MapFrom(
                    src => src.Merchant.Name
                    ))
            .ForMember(
                x => x.Currency, 
                options => options.MapFrom(
                    src => src.Merchant.Currency
                ));
    }
}