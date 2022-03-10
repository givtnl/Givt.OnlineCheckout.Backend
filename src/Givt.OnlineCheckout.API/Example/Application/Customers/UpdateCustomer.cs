namespace Givt.OnlineCheckout.API.Example.Application
{
    public class UpdateCustomerRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateCustomerResponse
    {
        public Guid Id { get; set; }
        public string StripeCustomerReference { get; set; }
    }
    
}
