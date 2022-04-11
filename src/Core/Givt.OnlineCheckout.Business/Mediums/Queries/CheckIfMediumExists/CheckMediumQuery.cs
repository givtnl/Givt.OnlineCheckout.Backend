using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.Mediums.Queries;

public class CheckMediumQuery: IRequest<bool>
{
    public MediumIdType MediumId { get; set; }
}