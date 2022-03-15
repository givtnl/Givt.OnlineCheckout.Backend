using Givt.OnlineCheckout.Persistance.Entities;
using Givt.OnlineCheckout.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Infrastructure.DbContexts;

public class OnlineCheckoutContext : DbContext
{
    public virtual DbSet<DataDonation> Donations { get; set; }
    public virtual DbSet<DataMerchant> Merchants { get; set; }
    public virtual DbSet<DataCustomer> Customers { get; set; } 
    public OnlineCheckoutContext(DbContextOptions options) : base(options)
    {
        
    }

    // TODO: put in static extension on context
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineCheckoutContext).Assembly);
    }
}