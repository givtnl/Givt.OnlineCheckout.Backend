namespace Givt.OnlineCheckout.Business.QR;
/// <summary>
/// base request for paginated queries
/// </summary>
public class ListQuery
{
    public string Filter { get; set; }
    public uint? Start { get; set; }
    public uint PageSize { get; set; } = 10;

}
