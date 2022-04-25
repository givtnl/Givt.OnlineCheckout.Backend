namespace Givt.OnlineCheckout.API.Models
{
    public class ListRequest
    {
        public string Filter { get; set; }
        public uint? Start { get; set; }
        public uint? PageSize { get; set; } = 10;

    }
}
