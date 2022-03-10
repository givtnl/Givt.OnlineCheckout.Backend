namespace Givt.OnlineCheckout.DataAccess.Infrastructure
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
