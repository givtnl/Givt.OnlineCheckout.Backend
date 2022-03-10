using Givt.OnlineCheckout.DataAccess.DataModels;
using Givt.OnlineCheckout.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.DataAccess;

public class OnlineCheckoutContext: DbContext
{
    public virtual DbSet<DataCustomer> Customers { get; set; }
    public virtual DbSet<DataMerchant> Merchants { get; set; }

    public OnlineCheckoutContext(DbContextOptions options): base(options)
    {
        
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        DbContextUpdateOperations.UpdateDates(ChangeTracker.Entries<AuditableEntity>());
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override int SaveChanges()
    {
        DbContextUpdateOperations.UpdateDates(ChangeTracker.Entries<AuditableEntity>());
        return base.SaveChanges(true);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        DbContextUpdateOperations.UpdateDates(ChangeTracker.Entries<AuditableEntity>());
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DbContextUpdateOperations.UpdateDates(ChangeTracker.Entries<AuditableEntity>());
        return base.SaveChangesAsync(true, cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}