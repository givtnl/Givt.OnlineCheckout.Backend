using AutoMapper;
using Givt.OnlineCheckout.Business.Extensions;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Persistance.Entities;
using System.Globalization;

namespace Givt.OnlineCheckout.Business.Mappings;

public class DataMediumMappingProfile : Profile
{
    public const string LanguageTag = "Language";
    public DataMediumMappingProfile()
    {
        CreateMap<MediumData, MediumFlattenedModel>()
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
                x => x.Country,
                options => options.MapFrom(
                    src => src.Organisation.CountryCode
                    ))
            .ForMember(
                x => x.PaymentMethods,
                options => options.MapFrom(
                    src => src.GetPaymentMethods()
                ))
            .ForMember(
                x => x.Currency,
                options => options.MapFrom(
                    src => src.Organisation.Country.Currency
                ))
            // select best Goal and ThankYou texts based on dst.Locale
            .ForMember(
                x => x.Goal,
                options => options.MapFrom(
                    (src, dest, _, context) => src.GetLocalisedText(nameof(MediumTexts.Goal), context.Items[LanguageTag] as string)
                ))
            .ForMember(
                x => x.ThankYou,
                options => options.MapFrom(
                    (src, dest, _, context) => src.GetLocalisedText(nameof(MediumTexts.ThankYou), context.Items[LanguageTag] as string)
                ))
            .ForMember(x => x.OrganisationLogoLink,
                options => options.MapFrom(
                    (src, dest) => src.Organisation.LogoImageLink
                ))
            ;
    }
}