using AutoMapper;
using Givt.OnlineCheckout.Business.Extensions;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.QR.Organisations.Update;
using Givt.OnlineCheckout.Persistance.Entities;
using System.Globalization;

namespace Givt.OnlineCheckout.Business.Mappings;

public class DataOrganisationMappingProfile : Profile
{
    public DataOrganisationMappingProfile()
    {
        // Domain -> Business
        CreateMap<OrganisationData, OrganisationDetailModel>();
        CreateMap<OrganisationData, OrganisationModel>()
            .ForMember(
                dst => dst.Country,
                options => options.MapFrom(src => src.CountryCode)
             )
            .ForMember(
                x => x.PaymentMethods,
                options => options.MapFrom(
                    src => src.GetPaymentMethods()
            ))
            ;

        CreateMap<OrganisationTexts, LocalisableTextModel>();

        CreateMap<MediumData, MediumDetailModel>()
            .ForMember(
                dst => dst.Amounts,
                options => options.MapFrom(
                    src => src.Amounts.Split(',', StringSplitOptions.None).Select(str => decimal.Parse(str, CultureInfo.InvariantCulture)).ToList()
                )
            );
        CreateMap<MediumTexts, LocalisableTextModel>();

        // Business -> Domain
        CreateMap<UpdateOrganisationQuery, OrganisationData>()
            .ForMember(
                dst => dst.Country, 
                options => options.Ignore())
            .ForMember(
                dst => dst.CountryCode,
                options => options.MapFrom(src => src.Country)
             )
            .ForMember(
                x => x.PaymentMethods,
                options => options.MapFrom(
                    src => src.PaymentMethods.MapPaymentMethods())
            );
    }
}