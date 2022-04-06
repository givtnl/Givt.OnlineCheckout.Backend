using AutoMapper;
using Givt.OnlineCheckout.Business.Reports;
using Givt.OnlineCheckout.Integrations.Interfaces;

namespace Givt.OnlineCheckout.Business.Mappings
{
    public class DonationReportMappingProfile: Profile
    {
        public DonationReportMappingProfile()
        {            
            CreateMap<IFileData, GetDonationReportCommandResponse>();
        }

    }
}
