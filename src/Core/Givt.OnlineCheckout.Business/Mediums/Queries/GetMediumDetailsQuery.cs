using Givt.OnlineCheckout.Application.Models;
using MediatR;

namespace Givt.OnlineCheckout.Application.Mediums.Queries;

public class GetMediumDetailsQuery: IRequest<MediumDetailModel>
{
    public string MediumId { get; set; }
}