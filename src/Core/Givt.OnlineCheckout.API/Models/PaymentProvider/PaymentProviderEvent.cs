using Microsoft.Extensions.Primitives;

namespace Givt.OnlineCheckout.API.Models.PaymentProvider;

public class PaymentProviderEvent
{
    public Stream Stream { get; set; }
    public IDictionary<string, StringValues> MetaData { get; set; }
}