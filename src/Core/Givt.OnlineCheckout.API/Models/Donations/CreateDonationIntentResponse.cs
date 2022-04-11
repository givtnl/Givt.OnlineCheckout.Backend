namespace Givt.OnlineCheckout.API.Models.Donations;

public class CreateDonationIntentResponse
{
    public string PaymentMethodId { get; set; }
    public string Token { get; set; }
}