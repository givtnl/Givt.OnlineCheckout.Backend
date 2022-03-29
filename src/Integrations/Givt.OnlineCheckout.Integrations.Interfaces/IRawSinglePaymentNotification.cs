using MediatR;
using Microsoft.Extensions.Primitives;

namespace Givt.OnlineCheckout.Integrations.Interfaces;

public interface IRawSinglePaymentNotification : INotification
{
    string RawData { get; set; }
    IDictionary<string, StringValues> MetaData { get; set; }
}
