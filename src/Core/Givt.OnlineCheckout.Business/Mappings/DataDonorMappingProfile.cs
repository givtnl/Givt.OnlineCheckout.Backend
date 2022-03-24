using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Business.Mappings;

public class DataDonorMappingProfile : Profile
{
    public DataDonorMappingProfile()
    {
        // Domain -> Business
        CreateMap<DonorData, DonorDetailModel>();
    }
}
