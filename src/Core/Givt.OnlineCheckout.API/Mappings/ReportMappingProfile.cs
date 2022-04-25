using AutoMapper;
using Givt.OnlineCheckout.API.Models.Reports;
using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Business.QR.Reports.Get;
using Givt.OnlineCheckout.Business.QR.Reports.Send;
using System.Security.Claims;

namespace Givt.OnlineCheckout.API.Mappings;

public class ReportMappingProfile : Profile
{
    public const string S_TOKENHANDLER = "TokenHandler";
    public const string S_USER = "User";
    public ReportMappingProfile()
    {
        CreateMap<GetDonationReportRequest, GetDonationReportCommand>()
            .ForMember(x => x.Language, 
                options => options.MapFrom(src => src.Locale))
            .ForMember(x => x.TransactionReference,
                options => options.MapFrom(
                    (src, _, _, context) =>
                        (context.Items[S_TOKENHANDLER] as JwtTokenHandler)?.GetTransactionReference(context.Items[S_USER] as ClaimsPrincipal)
                ));

        CreateMap<GetDonationReportCommandResponse, GetDonationReportResponse>();


        CreateMap<SendDonationReportRequest, SendDonationReportNotification>()
            .ForMember(x => x.Language, 
                options => options.MapFrom(src => src.Locale))
            .ForMember(x => x.TransactionReference,
                options => options.MapFrom(
                    (src, _, _, context) =>
                        (context.Items[S_TOKENHANDLER] as JwtTokenHandler)?.GetTransactionReference(context.Items[S_USER] as ClaimsPrincipal)
                ));
    }
}