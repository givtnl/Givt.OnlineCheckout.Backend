﻿using AutoMapper;
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
                .ForMember(dst => dst.OrganisationName,
                    options => options.MapFrom(
                        (src) => src.Name));
            // main mapping
            CreateMap<DonationData, DonationReport>()
                .IncludeMembers(src => src.Medium.Organisation)
                .ForMember(dst => dst.Locale,
                    options => options.MapFrom(
                        (src, dst, _, context) => context.Items[LanguageTag] as string))
                .ForMember(dst => dst.OrganisationName,
                    options => options.MapFrom(
                        (src) => src.Medium.Organisation.Name))
                .ForMember(dst => dst.Goal,
                    options => options.MapFrom(
                        (src, dest, _, context) => src.GetGoal(context.Items[LanguageTag] as string)))
                .ForMember(dst => dst.Timestamp,
                    options => options.MapFrom(
                        (src, dest, _, context) => src.GetTimestamp(context.Items[LanguageTag] as string)))
                .ForMember(dst => dst.Amount,
                    options => options.MapFrom(
                        (src) => src.Amount.ToString("F2")));


            CreateMap<DonationData, DonationsReport>()
                    .ForMember(dst => dst.Organisations,
                        options => options.MapFrom(
                            (src, dest, _, context) => src.GetOrganisations(context.Items[LanguageTag] as string))
                    )
                    .ForMember(dst => dst.Locale,
                        options => options.MapFrom(
                            (src, dst, _, context) => context.Items[LanguageTag] as string));
        }

    }
}
