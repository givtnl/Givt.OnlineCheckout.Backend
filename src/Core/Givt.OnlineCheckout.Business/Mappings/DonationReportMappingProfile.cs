﻿using System.Globalization;
using AutoMapper;
using Givt.OnlineCheckout.Business.Extensions;
using Givt.OnlineCheckout.Business.QR.Reports.Get;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Business.Mappings
{
    public class DonationReportMappingProfile : Profile
    {
        public const string LanguageTag = "Language";
        public DonationReportMappingProfile()
        {
            // Integrations -> Business
            CreateMap<IFileData, GetDonationReportCommandResponse>();

            // Business -> Integrations
            // child mapping
            CreateMap<OrganisationData, DonationReport>()
                .ForMember(dst => dst.OrganisationName, options => options.MapFrom((src) => src.Name))
                ;
            CreateMap<CountryData, GivtInfo>()
                .ForMember(dst => dst.Name, options => options.MapFrom((src) => src.GivtName))
                .ForMember(dst => dst.Address, options => options.MapFrom((src) => src.GivtAddress))
                .ForMember(dst => dst.PhoneNumber, options => options.MapFrom((src) => src.GivtPhoneNumber))
                .ForMember(dst => dst.Email, options => options.MapFrom((src) => src.GivtEmail))
                .ForMember(dst => dst.Website, options => options.MapFrom((src) => src.GivtWebsite))
                ;
            // main mapping
            CreateMap<DonationData, DonationReport>()
                .IncludeMembers(src => src.Medium.Organisation)
                .ForMember(dst => dst.Locale,
                    options => options.MapFrom(
                        (src, dst, _, context) => (context.Items[LanguageTag] as CultureInfo)?.TwoLetterISOLanguageName))
                .ForMember(dst => dst.OrganisationName,
                    options => options.MapFrom(
                        (src) => src.Medium.Organisation.Name))
                .ForMember(dst => dst.Title,
                    options => options.MapFrom(
                        (src, dest, _, context) => src.GetTitle((context.Items[LanguageTag] as CultureInfo)?.TwoLetterISOLanguageName)))
                .ForMember(dst => dst.Goal,
                    options => options.MapFrom(
                        (src, dest, _, context) => src.GetGoal((context.Items[LanguageTag] as CultureInfo)?.TwoLetterISOLanguageName)))
                .ForMember(dst => dst.Timestamp,
                    options => options.MapFrom(
                        (src, _, _, _) => src.TransactionDate))
                .ForMember(dst => dst.Amount,
                    options => options.MapFrom(
                        (src) => ((int)(src.Amount * 100)) / 100.0m))
                .ForMember(dst => dst.Givt, options => options.MapFrom((src) => src.Medium.Organisation.Country))
                ;

            CreateMap<DonationData, DonationsReport>()
                .ForMember(dst => dst.Organisations,
                    options => options.MapFrom(
                        (src, dest, _, context) => src.GetOrganisations((context.Items[LanguageTag] as CultureInfo)?.TwoLetterISOLanguageName))
                )
                .ForMember(dst => dst.CampaignName,
                    options => options.MapFrom(
                        (src, dest, _, context) => src.Medium.GetLocalisedText(nameof(MediumTexts.Title), (context.Items[LanguageTag] as CultureInfo)?.TwoLetterISOLanguageName))
                )
                .ForMember(dst => dst.Locale,
                    options => options.MapFrom(
                        (src, dst, _, context) => (context.Items[LanguageTag] as CultureInfo)?.TwoLetterISOLanguageName))
                .ForMember(dst => dst.Givt, options => options.MapFrom((src) => src.Medium.Organisation.Country))
                ;
        }

    }
}
