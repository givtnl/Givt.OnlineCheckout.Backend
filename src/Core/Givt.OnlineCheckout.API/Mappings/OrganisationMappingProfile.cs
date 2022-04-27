using AutoMapper;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.API.Models.Organisations.Get;
using Givt.OnlineCheckout.API.Models.Organisations.List;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.QR.Organisations.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Update;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.API.Mappings;

public class OrganisationMappingProfile : Profile
{
    public OrganisationMappingProfile()
    {
        // Application -> Business
        CreateMap<ListOrganisationsRequest, ListOrganisationsQuery>();
        CreateMap<OrganisationInfoBase, CreateOrganisationQuery>();
        CreateMap<GetOrganisationRequest, ReadOrganisationQuery>();
        CreateMap<UpdateOrganisationRequest, UpdateOrganisationQuery>();

        CreateMap<ListOrganisationTextsRequest, ListOrganisationTextsQuery>();
        CreateMap<CreateOrganisationTextsRequest, CreateOrganisationTextsCommand>()
            .ForMember(dst => dst.OrganisationId,
                options => options.MapFrom((src, _, _, context) => (long)context.Items[Keys.ORGANISATION_ID]))
            .ForMember(dst => dst.LanguageId,
                options => options.MapFrom((src, _, _, context) => (string)context.Items[Keys.LANGUAGE_ID]));
        CreateMap<UpdateOrganisationTextsRequest, UpdateOrganisationTextsCommand>()
            .ForMember(dst => dst.OrganisationId,
                options => options.MapFrom((src, _, _, context) => (long)context.Items[Keys.ORGANISATION_ID]))
            .ForMember(dst => dst.LanguageId,
                options => options.MapFrom((src, _, _, context) => (string)context.Items[Keys.LANGUAGE_ID]));

        CreateMap<ListOrganisationMediumsRequest, ListOrganisationMediumsQuery>();

        CreateMap<ListOrganisationMediumTextsRequest, ListOrganisationMediumTextsQuery>();
        CreateMap<CreateOrganisationMediumTextsRequest, CreateOrganisationMediumTextsCommand>()
              .ForMember(dst => dst.OrganisationId,
                    options => options.MapFrom((src, _, _, context) => (long)context.Items[Keys.ORGANISATION_ID]))
              .ForMember(dst => dst.MediumId,
                    options => options.MapFrom((src, _, _, context) => (string)context.Items[Keys.MEDIUM_ID]))
              .ForMember(dst => dst.LanguageId,
                    options => options.MapFrom((src, _, _, context) => (string)context.Items[Keys.LANGUAGE_ID]));


        // Business -> Application
        CreateMap<PaymentMethod, String>().ConvertUsing(e => e.ToString().ToLower());

        CreateMap<OrganisationModel, OrganisationInfo>();

        CreateMap<LocalisableTextModel, LocalisableTextsResponse>();
        CreateMap<CreateOrganisationTextsResult, CreateOrganisationTextsResponse>();
        CreateMap<ReadOrganisationTextsResult, GetOrganisationTextsResponse>();
        CreateMap<UpdateOrganisationTextsResult, UpdateOrganisationTextsResponse>();

        CreateMap<MediumDetailModel, MediumInfo>();
    }

}