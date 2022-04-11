namespace Givt.OnlineCheckout.Business.Donations;

public class CreateDonationIntentCommandResponse
{
    public string PaymentIntentSecret { get; set; }
    public string TransactionReference { get; set; }
}