using System.Globalization;
using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Business.Mappings;

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
                    src => src.Organisation.Name
                    ))
            .ForMember(
                x => x.Currency, 
                options => options.MapFrom(
                    src => src.Organisation.Currency
                ));
    }
}