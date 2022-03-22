namespace Givt.OnlineCheckout.API.Models
{
    public class CustomerDetailModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string StripeCustomerReference { get; set; }
    }
}