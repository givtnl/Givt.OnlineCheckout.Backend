namespace Givt.OnlineCheckout.Business.QR.Donations.Create;

public class CreateDonationIntentCommandResponse
{
    public string PaymentIntentSecret { get; set; }
    public string TransactionReference { get; set; }
}