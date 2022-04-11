using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.Mediums.Queries;

public class GetMediumDetailsQuery: IRequest<MediumDetailModel>
{
    public MediumIdType MediumId { get; set; }
    public string Language { get; set; }
}