using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Integrations.Interfaces;

/// <summary>
/// Our definition of a Payment Service Provider. 
/// </summary>
public interface ISinglePaymentService
{
    // TODO: change the return value to include both the token for the UI (client secret) AND a transaction/payment reference
    Task<string> CreatePaymentIntent(string currency, decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod);   
}