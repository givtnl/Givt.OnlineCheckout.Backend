﻿using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class OrganisationDataConfiguration : IEntityTypeConfiguration<OrganisationData>
    {
        public void Configure(EntityTypeBuilder<OrganisationData> builder)
        {
            builder
                .Property(e => e.Name)
                .HasMaxLength(35);

            builder
                .Property(e => e.PaymentProviderAccountReference)
                .HasMaxLength(50);

            builder
                .Property(e => e.Namespace)
                .HasMaxLength(20);

            builder
                .Property(e => e.Currency)
                .HasMaxLength(3);


            builder.HasMany(e => e.Texts)
                .WithOne(t => t.Organisation)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.LogoImageLink)
                .HasMaxLength(100);

            builder
                .HasIndex(e => e.Namespace);
        }
    }
}
