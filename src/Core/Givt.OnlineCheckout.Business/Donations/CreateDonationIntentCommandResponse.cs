namespace Givt.OnlineCheckout.Business.Donations;

public class CreateDonationIntentCommandResponse
{
    public string PaymentIntentSecret { get; set; }
    internal string TransactionReference { get; set; }
}