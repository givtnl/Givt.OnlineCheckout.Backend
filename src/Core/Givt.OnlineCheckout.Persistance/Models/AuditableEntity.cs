namespace Givt.OnlineCheckout.Persistance.Models
{
    public abstract class AuditableEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
