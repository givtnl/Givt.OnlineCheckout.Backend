﻿// <auto-generated />
using System;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    [DbContext(typeof(OnlineCheckoutContext))]
    partial class OnlineCheckoutContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.CountryData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ApplicationFeeFixedAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ApplicationFeePercentage")
                        .HasColumnType("numeric");

                    b.Property<string>("CountryCode")
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<string>("Currency")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("PaymentMethods")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("CountryCode");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DonationData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Currency")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DonorId")
                        .HasColumnType("uuid");

                    b.Property<long?>("MediumId")
                        .HasColumnType("bigint");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint");

                    b.Property<int>("TimezoneOffset")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TransactionReference")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("DonorId");

                    b.HasIndex("MediumId");

                    b.HasIndex("TransactionReference");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DonorData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("Donors");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.MediumData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Amounts")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Medium")
                        .HasMaxLength(33)
                        .HasColumnType("character varying(33)");

                    b.Property<long>("OrganisationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Mediums");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.MediumTexts", b =>
                {
                    b.Property<long>("MediumId")
                        .HasColumnType("bigint");

                    b.Property<string>("LanguageId")
                        .HasColumnType("text");

                    b.Property<string>("Goal")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("ThankYou")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.HasKey("MediumId", "LanguageId");

                    b.ToTable("MediumTexts");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.OrganisationData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("CharityNumber")
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HmrcReference")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("LogoImageLink")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.Property<string>("Namespace")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<decimal>("PaymentMethods")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("PaymentProviderAccountReference")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("RSIN")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("TaxDeductable")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Namespace");

                    b.ToTable("Organisations");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.OrganisationTexts", b =>
                {
                    b.Property<long>("OrganisationId")
                        .HasColumnType("bigint");

                    b.Property<string>("LanguageId")
                        .HasColumnType("text");

                    b.Property<string>("Goal")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("ThankYou")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.HasKey("OrganisationId", "LanguageId");

                    b.ToTable("OrganisationTexts");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DonationData", b =>
                {
                    b.HasOne("Givt.OnlineCheckout.Persistance.Entities.DonorData", "Donor")
                        .WithMany("Donations")
                        .HasForeignKey("DonorId");

                    b.HasOne("Givt.OnlineCheckout.Persistance.Entities.MediumData", "Medium")
                        .WithMany()
                        .HasForeignKey("MediumId");

                    b.Navigation("Donor");

                    b.Navigation("Medium");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.MediumData", b =>
                {
                    b.HasOne("Givt.OnlineCheckout.Persistance.Entities.OrganisationData", "Organisation")
                        .WithMany("Mediums")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisation");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.MediumTexts", b =>
                {
                    b.HasOne("Givt.OnlineCheckout.Persistance.Entities.MediumData", "Medium")
                        .WithMany("Texts")
                        .HasForeignKey("MediumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medium");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.OrganisationData", b =>
                {
                    b.HasOne("Givt.OnlineCheckout.Persistance.Entities.CountryData", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.OrganisationTexts", b =>
                {
                    b.HasOne("Givt.OnlineCheckout.Persistance.Entities.OrganisationData", "Organisation")
                        .WithMany("Texts")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisation");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.DonorData", b =>
                {
                    b.Navigation("Donations");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.MediumData", b =>
                {
                    b.Navigation("Texts");
                });

            modelBuilder.Entity("Givt.OnlineCheckout.Persistance.Entities.OrganisationData", b =>
                {
                    b.Navigation("Mediums");

                    b.Navigation("Texts");
                });
#pragma warning restore 612, 618
        }
    }
}
