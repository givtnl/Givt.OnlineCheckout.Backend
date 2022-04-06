namespace Givt.OnlineCheckout.Integrations.Interfaces
{
    public interface ISinglePaymentServicePaymentIntent
    {
        string TransactionReference{get;}
        string ClientToken { get; }
    }
}
