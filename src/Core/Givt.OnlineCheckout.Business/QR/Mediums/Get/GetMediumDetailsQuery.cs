using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Mediums.Check;

public class GetMediumDetailsQuery: IRequest<MediumDetailModelExtended>
{
    public MediumIdType MediumId { get; set; }
    public string Language { get; set; }
}