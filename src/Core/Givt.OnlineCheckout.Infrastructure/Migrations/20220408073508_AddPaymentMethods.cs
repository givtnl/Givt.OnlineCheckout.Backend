using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class AddPaymentMethods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Donations");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Organisations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PaymentMethods",
                table: "Organisations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CountryData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    PaymentMethods = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_CountryId",
                table: "Organisations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryData_CountryCode",
                table: "CountryData",
                column: "CountryCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_CountryData_CountryId",
                table: "Organisations",
                column: "CountryId",
                principalTable: "CountryData",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_CountryData_CountryId",
                table: "Organisations");

            migrationBuilder.DropTable(
                name: "CountryData");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_CountryId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "PaymentMethods",
                table: "Organisations");

            migrationBuilder.AddColumn<string>(
                name: "LanguageId",
                table: "Donations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
