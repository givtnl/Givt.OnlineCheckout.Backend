using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.ApplicationFee;

public class GetApplicationFeeQuery: IRequest<GetApplicationFeeQueryResponse>
{
    public MediumIdType MediumIdType { get; set; }
}