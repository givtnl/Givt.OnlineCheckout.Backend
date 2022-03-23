using AutoMapper;
using Givt.OnlineCheckout.Business.Donors.Commands;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.API.Models.Donors;

namespace Givt.OnlineCheckout.API.Mappings
{
    public class DonorMappingProfile : Profile
    {

        public DonorMappingProfile()
        {
            // Application -> Business
            CreateMap<CreateDonorRequest, CreateDonorCommand>();
            CreateMap<UpdateDonorRequest, UpdateDonorCommand>();

            // Business -> Application
            CreateMap<DonorDetailModel, CreateDonorResponse>();
            CreateMap<DonorDetailModel, UpdateDonorResponse>();
        }
    }
}
