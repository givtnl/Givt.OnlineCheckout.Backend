using AutoMapper;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.API.Models.Organisations.Create;
using Givt.OnlineCheckout.API.Models.Organisations.Get;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Create;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Get;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Create;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Delete;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Get;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.List;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Update;
using Givt.OnlineCheckout.API.Models.Organisations.Mediums.Update;
using Givt.OnlineCheckout.API.Models.Organisations.Texts.Create;
using Givt.OnlineCheckout.API.Models.Organisations.Texts.Delete;
using Givt.OnlineCheckout.API.Models.Organisations.Texts.Get;
using Givt.OnlineCheckout.API.Models.Organisations.Texts.List;
using Givt.OnlineCheckout.API.Models.Organisations.Texts.Update;
using Givt.OnlineCheckout.API.Models.Organisations.Update;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.QR.Organisations.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Delete;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.List;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Update;
using Givt.OnlineCheckout.Business.QR.Organisations.Read;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Create;
using Givt.OnlineCheckout.Business.QR.Organisations.Texts.Delete;
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
        CreateMap<ListRequest, ListOrganisationsQuery>();
        CreateMap<CreateOrganisationRequest, CreateOrganisationCommand>();
        CreateMap<GetOrganisationRequest, ReadOrganisationQuery>();
        CreateMap<OrganisationInfoCore, CreateOrganisationCommand>();
        CreateMap<UpdateOrganisationRequest, UpdateOrganisationCommand>();

        CreateMap<ListOrganisationTextsRequest, ListOrganisationTextsQuery>();
        CreateMap<CreateOrganisationTextsRequest, CreateOrganisationTextsCommand>();
        CreateMap<GetOrganisationTextsRequest, ReadOrganisationTextsQuery>();
        CreateMap<UpdateOrganisationTextsRequest, UpdateOrganisationTextsCommand>();
        CreateMap<DeleteOrganisationTextsRequest, DeleteOrganisationTextsCommand>();

        CreateMap<ListOrganisationMediumsRequest, ListOrganisationMediumsQuery>();
        CreateMap<CreateOrganisationMediumRequest, CreateOrganisationMediumCommand>();
        CreateMap<GetOrganisationMediumRequest, ReadOrganisationMediumQuery>();
        CreateMap<UpdateOrganisationMediumRequest, UpdateOrganisationMediumCommand>();

        CreateMap<ListOrganisationMediumTextsRequest, ListOrganisationMediumTextsQuery>();
        CreateMap<CreateOrganisationMediumTextsRequest, CreateOrganisationMediumTextsCommand>();
        CreateMap<GetOrganisationMediumTextsRequest, ReadOrganisationMediumTextsQuery>();
        CreateMap<UpdateOrganisationMediumTextsRequest, UpdateOrganisationMediumTextsCommand>();
        CreateMap<DeleteOrganisationMediumTextsRequest, DeleteOrganisationMediumTextsCommand>();


        // Business -> Application
        CreateMap<PaymentMethod, String>().ConvertUsing(e => e.ToString().ToLower());

        CreateMap<OrganisationDetailModel, OrganisationInfo>();
        CreateMap<OrganisationDetailModel, CreateOrganisationResponse>();
        CreateMap<OrganisationDetailModel, GetOrganisationResponse>();
        CreateMap<OrganisationDetailModel, UpdateOrganisationResponse>();

        CreateMap<LocalisableTextModel, LocalisableTextsInfo>();
        CreateMap<CreateOrganisationTextsResult, CreateOrganisationTextsResponse>();
        CreateMap<ReadOrganisationTextsResult, GetOrganisationTextsResponse>();
        CreateMap<UpdateOrganisationTextsResult, UpdateOrganisationTextsResponse>();

        CreateMap<MediumDetailModel, MediumInfo>();
        CreateMap<MediumDetailModel, CreateOrganisationMediumResponse>();
        CreateMap<MediumDetailModel, GetOrganisationMediumResponse>();
        CreateMap<MediumDetailModel, UpdateOrganisationMediumResponse>();

        CreateMap<CreateOrganisationMediumTextsResult, CreateOrganisationMediumTextsResponse>();
        CreateMap<ReadOrganisationMediumTextsResult, GetOrganisationMediumTextsResponse>();
        CreateMap<UpdateOrganisationMediumTextsResult, UpdateOrganisationMediumTextsResponse>();
        
    }

}