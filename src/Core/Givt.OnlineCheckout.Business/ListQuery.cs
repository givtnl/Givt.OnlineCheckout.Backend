namespace Givt.OnlineCheckout.Business
{
    public class ListQuery
    {
        public string Filter { get; set; }
        public uint? Start { get; set; }
        public uint PageSize { get; set; } = 10;

    }
}
