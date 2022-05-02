using AutoMapper;
using Givt.OnlineCheckout.Business.Extensions;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.QR.Organisations.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Update;
using Givt.OnlineCheckout.Persistance.Entities;
using System.Globalization;

namespace Givt.OnlineCheckout.Business.Mappings;

public class DataOrganisationMappingProfile : Profile
{
    public DataOrganisationMappingProfile()
    {
        // Domain -> Business
        CreateMap<OrganisationData, OrganisationFlattenedModel>();
        CreateMap<OrganisationData, OrganisationDetailModel>()
            .ForMember(
                dst => dst.OrganisationId,
                options => options.MapFrom(src => src.Id)
             )
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

        CreateMap<MediumData, MediumDetailModel>()
            .ForMember(
                dst => dst.Amounts,
                options => options.MapFrom(
                    src => src.Amounts.Split(',', StringSplitOptions.None).Select(str => decimal.Parse(str, CultureInfo.InvariantCulture)).ToList()
                )
            );
        

        CreateMap<MediumTexts, MediumTextModel>();
        CreateMap<MediumTexts, CreateOrganisationMediumTextsResult>();
        CreateMap<MediumTexts, ReadOrganisationMediumTextsResult>();
        CreateMap<MediumTexts, UpdateOrganisationMediumTextsResult>();

        // Business -> Domain
        CreateMap<CreateOrganisationCommand, OrganisationData>()
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
        CreateMap<UpdateOrganisationCommand, OrganisationData>()
            .ForMember(
                dst => dst.Id,
                options => options.MapFrom(src => src.OrganisationId))
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
        
        CreateMap<CreateOrganisationMediumCommand, MediumData>()
            .ForMember(
                dst => dst.Amounts,
                options => options.MapFrom(
                    src => string.Join(',', src.Amounts))
            );
        CreateMap<UpdateOrganisationMediumCommand, MediumData>()
            .ForMember(dst => dst.OrganisationId, options => options.Ignore()) // protect from changes
            .ForMember(dst => dst.Medium, options => options.Ignore()) // protect from changes
            .ForMember(
                dst => dst.Amounts,
                options => options.MapFrom(
                    src => string.Join(',', src.Amounts))
            );            

        CreateMap<CreateOrganisationMediumTextsCommand, MediumTexts>()
            .ForMember(dst => dst.MediumId, options => options.Ignore());
        CreateMap<UpdateOrganisationMediumTextsCommand, MediumTexts>()
            .ForMember(dst => dst.MediumId, options => options.Ignore());
    }
}