﻿// <auto-generated />
using System;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    [DbContext(typeof(OnlineCheckoutContext))]
    [Migration("20220316101914_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DataCustomer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DataDonation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("DataCustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PaymentProviderTransactionReference")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DataCustomerId");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DataMedium", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal[]>("Amounts")
                        .IsRequired()
                        .HasColumnType("numeric[]");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Goal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Medium")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("MerchantId")
                        .HasColumnType("bigint");

                    b.Property<string>("ThankYou")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.ToTable("DataMedium");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DataMerchant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Namespace")
                        .HasColumnType("text");

                    b.Property<string>("PaymentProviderAccountReference")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DataDonation", b =>
                {
                    b.HasOne("Givt.OnlineCheckout.Persistance.Entities.DataCustomer", null)
                        .WithMany("Donations")
                        .HasForeignKey("DataCustomerId");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DataMedium", b =>
                {
                    b.HasOne("Givt.OnlineCheckout.Persistance.Entities.DataMerchant", "Merchant")
                        .WithMany("Mediums")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DataCustomer", b =>
                {
                    b.Navigation("Donations");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DataMerchant", b =>
                {
                    b.Navigation("Mediums");
                });
#pragma warning restore 612, 618
        }
    }
}
