using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Mediums.Check;

public class GetMediumDetailsQuery: IRequest<MediumFlattenedModel>
{
    public MediumIdType MediumId { get; set; }
    public string Language { get; set; }
}