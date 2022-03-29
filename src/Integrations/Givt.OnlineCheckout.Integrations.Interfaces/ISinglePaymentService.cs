using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using MediatR;

namespace Givt.OnlineCheckout.Integrations.Interfaces;

/// <summary>
/// Our definition of a Payment Service Provider. 
/// </summary>
public interface ISinglePaymentService
{
    Task<string> CreatePaymentIntent(string currency, decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod);   
}