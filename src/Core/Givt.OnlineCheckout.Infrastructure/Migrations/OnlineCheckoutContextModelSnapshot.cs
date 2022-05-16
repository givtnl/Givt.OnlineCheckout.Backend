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
                    b.Property<string>("CountryCode")
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<decimal>("ApplicationFeeFixedAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ApplicationFeePercentage")
                        .HasColumnType("numeric");

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("Currency")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GivtAddress")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("GivtEmail")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("GivtName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("GivtPhoneNumber")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("GivtPrivacyPolicy")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("GivtWantKnowMore")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("GivtWebsite")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Locale")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<ulong?>("PaymentMethods")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("CountryCode");

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

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("Currency")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DonorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Fingerprint")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<long?>("MediumId")
                        .HasColumnType("bigint");

                    b.Property<byte?>("PaymentMethod")
                        .HasColumnType("smallint");

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

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

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

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

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

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("Goal")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("ThankYou")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("Title")
                        .HasMaxLength(175)
                        .HasColumnType("character varying(175)");

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

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("CountryCode")
                        .HasColumnType("character varying(2)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HmrcReference")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("LogoImageLink")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .HasMaxLength(175)
                        .HasColumnType("character varying(175)");

                    b.Property<string>("Namespace")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<ulong?>("PaymentMethods")
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

                    b.HasIndex("CountryCode");

                    b.HasIndex("Namespace");

                    b.ToTable("Organisations");
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
                        .HasForeignKey("CountryCode");

                    b.Navigation("Country");
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
                });
#pragma warning restore 612, 618
        }
    }
}
