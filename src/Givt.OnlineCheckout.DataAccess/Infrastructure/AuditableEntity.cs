namespace Givt.OnlineCheckout.DataAccess.Infrastructure
{
    public abstract class AuditableEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
