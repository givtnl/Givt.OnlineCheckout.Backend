using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class RenameEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Customers_DataCustomerId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Mediums_Merchants_MerchantId",
                table: "Mediums");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropColumn(
                name: "PaymentProviderTransactionReference",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "MerchantId",
                table: "Mediums",
                newName: "OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_Mediums_MerchantId",
                table: "Mediums",
                newName: "IX_Mediums_OrganisationId");

            migrationBuilder.RenameColumn(
                name: "DataCustomerId",
                table: "Donations",
                newName: "DonorId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_DataCustomerId",
                table: "Donations",
                newName: "IX_Donations_DonorId");

            migrationBuilder.AlterColumn<string>(
                name: "ThankYou",
                table: "Mediums",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Medium",
                table: "Mediums",
                type: "character varying(33)",
                maxLength: 33,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Goal",
                table: "Mediums",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Amounts",
                table: "Mediums",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Donations",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Donations",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionReference",
                table: "Donations",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: true),
                    PaymentProviderAccountReference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Namespace = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donors_Email",
                table: "Donors",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_Namespace",
                table: "Organisations",
                column: "Namespace");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Donors_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "Donors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mediums_Organisations_OrganisationId",
                table: "Mediums",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Donors_DonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Mediums_Organisations_OrganisationId",
                table: "Mediums");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "TransactionReference",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "OrganisationId",
                table: "Mediums",
                newName: "MerchantId");

            migrationBuilder.RenameIndex(
                name: "IX_Mediums_OrganisationId",
                table: "Mediums",
                newName: "IX_Mediums_MerchantId");

            migrationBuilder.RenameColumn(
                name: "DonorId",
                table: "Donations",
                newName: "DataCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_DonorId",
                table: "Donations",
                newName: "IX_Donations_DataCustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "ThankYou",
                table: "Mediums",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Medium",
                table: "Mediums",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(33)",
                oldMaxLength: 33,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Goal",
                table: "Mediums",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Amounts",
                table: "Mediums",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentProviderTransactionReference",
                table: "Donations",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Currency = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Namespace = table.Column<string>(type: "text", nullable: true),
                    PaymentProviderAccountReference = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Customers_DataCustomerId",
                table: "Donations",
                column: "DataCustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mediums_Merchants_MerchantId",
                table: "Mediums",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
