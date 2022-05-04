using AutoMapper;
using Givt.OnlineCheckout.API.Models.Reports;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.QR.Reports.Get;
using Givt.OnlineCheckout.Business.QR.Reports.Send;
using System.Globalization;
using System.Security.Claims;

namespace Givt.OnlineCheckout.API.Mappings;

public class ReportMappingProfile : Profile
{
    public ReportMappingProfile()
    {
        CreateMap<GetDonationReportRequest, GetDonationReportCommand>()
            .ForMember(x => x.Culture,
                options => options.MapFrom(
                    (src, _, _, context) => context.Items[Keys.CULTURE] as CultureInfo
                ))
            .ForMember(x => x.TransactionReference,
                options => options.MapFrom(
                    (src, _, _, context) =>
                        (context.Items[Keys.TOKEN_HANDLER] as JwtTokenHandler)?.GetTransactionReference(context.Items[Keys.USER] as ClaimsPrincipal)
                ));

        CreateMap<GetDonationReportCommandResponse, GetDonationReportResponse>();


        CreateMap<SendDonationReportRequest, SendDonationReportNotification>()
            .ForMember(x => x.Culture,
                options => options.MapFrom(
                    (src, _, _, context) => context.Items[Keys.CULTURE] as CultureInfo
                ))
            .ForMember(x => x.TransactionReference,
                options => options.MapFrom(
                    (src, _, _, context) =>
                        (context.Items[Keys.TOKEN_HANDLER] as JwtTokenHandler)?.GetTransactionReference(context.Items[Keys.USER] as ClaimsPrincipal)
                ));
    }
}