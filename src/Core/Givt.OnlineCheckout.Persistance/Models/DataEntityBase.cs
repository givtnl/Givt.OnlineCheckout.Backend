using Givt.OnlineCheckout.Persistance.Interfaces;

namespace Givt.OnlineCheckout.Persistance.Models
{
    public class DataEntityBase<TId> : AuditableEntity, IDataEntity<TId>
    {
        public uint ConcurrencyToken { get; set; } 
        public TId Id { get; set; }
    }
}
