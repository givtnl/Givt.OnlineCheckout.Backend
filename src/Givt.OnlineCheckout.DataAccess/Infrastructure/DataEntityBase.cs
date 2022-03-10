namespace Givt.OnlineCheckout.DataAccess.Infrastructure
{
    public class DataEntityBase<TId> : AuditableEntity, IDataEntity<TId>
    {
        public TId Id { get; set; }
    }
}
