using AutoMapper;
using Givt.OnlineCheckout.API.Models.Reports;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.QR.Reports.Get;
using Givt.OnlineCheckout.Business.QR.Reports.Send;
using System.Security.Claims;

namespace Givt.OnlineCheckout.API.Mappings;

public class ReportMappingProfile : Profile
{
    public ReportMappingProfile()
    {
        CreateMap<GetDonationReportRequest, GetDonationReportCommand>()
            .ForMember(x => x.CurrentCulture, 
                options => options.MapFrom(src => src.CurrentCulture))
            .ForMember(x => x.TransactionReference,
                options => options.MapFrom(
                    (src, _, _, context) =>
                        (context.Items[Keys.TOKEN_HANDLER] as JwtTokenHandler)?.GetTransactionReference(context.Items[Keys.USER] as ClaimsPrincipal)
                ));

        CreateMap<GetDonationReportCommandResponse, GetDonationReportResponse>();


        CreateMap<SendDonationReportRequest, SendDonationReportNotification>()
            .ForMember(x => x.CurrentCulture, 
                options => options.MapFrom(src => src.CurrentCulture))
            .ForMember(x => x.TransactionReference,
                options => options.MapFrom(
                    (src, _, _, context) =>
                        (context.Items[Keys.TOKEN_HANDLER] as JwtTokenHandler)?.GetTransactionReference(context.Items[Keys.USER] as ClaimsPrincipal)
                ));
    }
}