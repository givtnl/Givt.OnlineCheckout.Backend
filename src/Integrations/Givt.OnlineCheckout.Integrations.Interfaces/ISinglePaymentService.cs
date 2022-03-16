using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Integrations.Interfaces;

public interface ISinglePaymentService
{
    Task<string> CreatePaymentIntent(decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod);
}