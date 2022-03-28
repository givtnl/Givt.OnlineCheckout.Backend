using MediatR;
using Microsoft.Extensions.Primitives;

namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public class RawSinglePaymentNotification : INotification
{
    public string RawData { get; set; }
    public IDictionary<string, StringValues> MetaData { get; set; }
}
