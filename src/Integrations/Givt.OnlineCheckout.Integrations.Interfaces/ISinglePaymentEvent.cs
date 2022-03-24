namespace Givt.OnlineCheckout.Integrations.Interfaces
{
    public interface ISinglePaymentEvent
    {
        string TransactionReference { get; }
        bool Succeeded { get; }
        bool Cancelled { get; }
    }
}
