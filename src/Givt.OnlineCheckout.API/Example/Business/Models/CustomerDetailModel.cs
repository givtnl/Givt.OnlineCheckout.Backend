namespace Givt.OnlineCheckout.API.Example.Business
{
    public class CustomerDetailModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string StripeCustomerReference { get; set; }
    }
}