﻿using AutoMapper;
using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.Business.Mediums.Queries;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.API.Mappings;

public class MediumMappingProfile : Profile
{
    public MediumMappingProfile()
    {
        CreateMap<GetMediumRequest, GetMediumDetailsQuery>()
            .ForMember(dst => dst.Language, options => options.MapFrom(src => src.Locale))
            .ForMember(dst => dst.MediumId, options => options.MapFrom(src => MediumIdType.FromString(src.Code)));
        CreateMap<GetMediumRequest, CheckMediumQuery>()
            .ForMember(dst => dst.MediumId, options => options.MapFrom(src => MediumIdType.FromString(src.Code)));
        CreateMap<MediumDetailModel, GetMediumResponse>()
            .ForMember(dst => dst.PaymentMethods,
                options => options.MapFrom(src => GetPaymentMethodsAsString(src.PaymentMethods)));
    }

    private string[] GetPaymentMethodsAsString(IEnumerable<PaymentMethod> paymentMethods)
    {
        var result = new List<string>();
        foreach (var paymentMethod in paymentMethods)
        {
            result.Add(paymentMethod.ToString().ToLowerInvariant());
        }
        result.Sort();
        return result.ToArray();
    }
}