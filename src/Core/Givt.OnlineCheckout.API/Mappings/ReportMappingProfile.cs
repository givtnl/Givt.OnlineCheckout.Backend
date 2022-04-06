using AutoMapper;
using Givt.OnlineCheckout.API.Models.Reports;
using Givt.OnlineCheckout.Business.Reports;

namespace Givt.OnlineCheckout.API.Mappings;

public class ReportMappingProfile : Profile
{
    public ReportMappingProfile()
    {
        CreateMap<GetDonationReportRequest, GetDonationReportCommand>();
        CreateMap<GetDonationReportCommandResponse, GetDonationReportResponse>();
    }
}