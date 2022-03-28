using MediatR;
using Microsoft.Extensions.Primitives;

namespace Givt.OnlineCheckout.API.Models.PaymentProvider;

public class PaymentProviderEvent : IRequest
{
    public String Content{ get; set; }
    public IDictionary<string, StringValues> MetaData { get; set; }
}