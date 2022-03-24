using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Microsoft.Extensions.Primitives;

namespace Givt.OnlineCheckout.Integrations.Interfaces;

public interface ISinglePaymentService
{
    Task<string> CreatePaymentIntent(string currency, decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod);
    Task<ISinglePaymentEvent> GetEventData(Stream s, IDictionary<string, StringValues> metaData);
}