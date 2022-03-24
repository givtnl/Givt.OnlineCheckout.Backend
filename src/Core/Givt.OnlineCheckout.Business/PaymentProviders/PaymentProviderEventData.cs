using MediatR;
using Microsoft.Extensions.Primitives;

namespace Givt.OnlineCheckout.Business.PaymentProviders
{
    public class PaymentProviderEventData : IRequest<Unit>
    {
        public Stream Stream { get; set; }
        public IDictionary<string, StringValues> MetaData { get; set; }
    }
}
