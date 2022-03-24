namespace Givt.OnlineCheckout.API.Models.Donors
{
    public class UpdateDonorRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateDonorResponse
    {
        public Guid Id { get; set; }
        public string CustomerReference { get; set; }
    }

}
