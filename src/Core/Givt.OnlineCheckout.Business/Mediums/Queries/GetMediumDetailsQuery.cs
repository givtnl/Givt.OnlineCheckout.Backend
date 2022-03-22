using Givt.OnlineCheckout.API.Models;
using MediatR;

namespace Givt.OnlineCheckout.API.Mediums.Queries;

public class GetMediumDetailsQuery: IRequest<MediumDetailModel>
{
    public string MediumId { get; set; }
}