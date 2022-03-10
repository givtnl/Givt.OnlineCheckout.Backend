namespace Givt.OnlineCheckout.Persistance.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
