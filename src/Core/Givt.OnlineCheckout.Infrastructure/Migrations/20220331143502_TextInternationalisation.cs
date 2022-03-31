using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class TextInternationalisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LanguageId",
                table: "Donations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MediumId",
                table: "Donations",
                type: "bigint",
                nullable: true);
            /* is already in the database???
            migrationBuilder.AddColumn<int>(
                name: "TimezoneOffset",
                table: "Donations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            */
            /* is already in the database???
            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Donations",
                type: "timestamp with time zone",
                nullable: true);
            */
            migrationBuilder.CreateTable(
                name: "MediumTexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MediumId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<string>(type: "text", nullable: true),
                    Goal = table.Column<string>(type: "text", nullable: true),
                    ThankYou = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediumTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediumTexts_Mediums_MediumId",
                        column: x => x.MediumId,
                        principalTable: "Mediums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationTexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganisationId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<string>(type: "text", nullable: true),
                    Goal = table.Column<string>(type: "text", nullable: true),
                    ThankYou = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganisationTexts_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_MediumId",
                table: "Donations",
                column: "MediumId");

            /* is already in the database???
            migrationBuilder.CreateIndex(
                name: "IX_Donations_TransactionReference",
                table: "Donations",
                column: "TransactionReference");
            */
            migrationBuilder.CreateIndex(
                name: "IX_MediumTexts_MediumId",
                table: "MediumTexts",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationTexts_OrganisationId",
                table: "OrganisationTexts",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Mediums_MediumId",
                table: "Donations",
                column: "MediumId",
                principalTable: "Mediums",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Mediums_MediumId",
                table: "Donations");

            migrationBuilder.DropTable(
                name: "MediumTexts");

            migrationBuilder.DropTable(
                name: "OrganisationTexts");

            migrationBuilder.DropIndex(
                name: "IX_Donations_MediumId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_TransactionReference",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "MediumId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "TimezoneOffset",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Donations");
        }
    }
}
