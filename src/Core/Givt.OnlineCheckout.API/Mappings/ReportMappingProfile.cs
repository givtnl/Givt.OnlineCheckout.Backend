using AutoMapper;
using Givt.OnlineCheckout.API.Models.Reports;
using Givt.OnlineCheckout.Business.Reports;

namespace Givt.OnlineCheckout.API.Mappings;

public class ReportMappingProfile : Profile
{
    public ReportMappingProfile()
    {
        CreateMap<GetDonationReportRequest, GetDonationReportCommand>().ForMember(
                x => x.Language, options => options.MapFrom(src => src.Locale));
        CreateMap<GetDonationReportCommandResponse, GetDonationReportResponse>();


        CreateMap<SendDonationReportRequest, SendDonationReportCommand>().ForMember(
                x => x.Language, options => options.MapFrom(src => src.Locale));
    }
}