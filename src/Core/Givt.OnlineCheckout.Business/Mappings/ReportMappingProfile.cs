using AutoMapper;
using Givt.OnlineCheckout.Business.Models.Report;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Business.Mappings;

public class ReportMappingProfile: Profile
{
    public ReportMappingProfile()
    {
        CreateMap<ReportDonations, SingleDonationReport>();
    }
}