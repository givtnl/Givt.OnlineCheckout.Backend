using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Integrations.Interfaces;

/// <summary>
/// Our definition of a Payment Service Provider. 
/// </summary>
public interface ISinglePaymentService
{
    Task<ISinglePaymentServicePaymentIntent> CreatePaymentIntent(string currency, decimal amount, decimal applicationFee, string description, string accountId, PaymentMethod paymentMethod);
}