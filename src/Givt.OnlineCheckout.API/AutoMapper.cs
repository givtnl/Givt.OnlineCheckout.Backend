using AutoMapper;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.DataAccess.DataModels;

namespace Givt.OnlineCheckout.DataAccess;

public class CommandToDomainMappingProfile: Profile
{
    public CommandToDomainMappingProfile()
    {
        CreateMap<DataOrganisation, OrganisationResponse>();
        CreateMap<DataOrganisation, OrganisationDetails>();
    }
}