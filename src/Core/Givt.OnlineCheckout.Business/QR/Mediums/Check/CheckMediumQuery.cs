using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Mediums.Check;

public class CheckMediumQuery: IRequest<bool>
{
    public MediumIdType MediumId { get; set; }
}