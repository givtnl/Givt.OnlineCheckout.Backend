using AutoMapper;
using Givt.OnlineCheckout.DataAccess.DataModels;

namespace Givt.OnlineCheckout.API.Example.Business.MappingProfiles.Domain
{
    public class DomainToBusinessMappingProfile : Profile
    {
        public DomainToBusinessMappingProfile()
        {
            CreateMap<DataMerchant, MerchantDetailModel>();
        }
    }
}
