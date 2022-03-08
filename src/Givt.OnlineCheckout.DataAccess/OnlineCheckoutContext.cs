using Givt.OnlineCheckout.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.DataAccess;

public class OnlineCheckoutContext: DbContext
{
    public virtual DbSet<DataOrganisation> Organisations { get; set; }

    public OnlineCheckoutContext(DbContextOptions options): base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataOrganisation>(entity => { });
    }
}