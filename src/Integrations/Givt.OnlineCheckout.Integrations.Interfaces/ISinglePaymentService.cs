using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Microsoft.Extensions.Primitives;

namespace Givt.OnlineCheckout.Integrations.Interfaces;

public interface ISinglePaymentService
{
    Task<string> CreatePaymentIntent(string currency, decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod);
    ISinglePaymentEvent GetEventData(string content, IDictionary<string, StringValues> metaData);
}