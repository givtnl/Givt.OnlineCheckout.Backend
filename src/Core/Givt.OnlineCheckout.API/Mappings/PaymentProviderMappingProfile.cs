using AutoMapper;
using Givt.OnlineCheckout.API.Models.PaymentProvider;
using Givt.OnlineCheckout.Business.PaymentProviders;

namespace Givt.OnlineCheckout.API.Mappings;

public class PaymentProviderMappingProfile : Profile
{
    public PaymentProviderMappingProfile()
    {
        // API -> Business
        CreateMap<PaymentProviderEvent, PaymentProviderEventData>();
        // Business -> API
        //... no data returned
    }
}