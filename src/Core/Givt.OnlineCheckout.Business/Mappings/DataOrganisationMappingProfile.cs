using AutoMapper;
using Givt.OnlineCheckout.Business.Extensions;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.Organisations;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Business.Mappings;

public class DataOrganisationMappingProfile : Profile
{
    public DataOrganisationMappingProfile()
    {
        // Domain -> Business
        CreateMap<OrganisationData, OrganisationDetailModel>();
        CreateMap<OrganisationData, OrganisationModel>()
            .ForMember(
                x => x.Currency,
                options => options.MapFrom(
                    src => src.Country.Currency
            ))
            .ForMember(
                x => x.PaymentMethods,
                options => options.MapFrom(
                    src => src.GetPaymentMethods()
            ))
            ;
        CreateMap<OrganisationTexts, LocalisableTextModel>();
    }
}