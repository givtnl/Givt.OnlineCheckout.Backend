using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class AddGivtInfoToCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GivtAddress",
                table: "Countries",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GivtEmail",
                table: "Countries",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GivtName",
                table: "Countries",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GivtPhoneNumber",
                table: "Countries",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GivtWebsite",
                table: "Countries",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Locale",
                table: "Countries",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GivtAddress",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "GivtEmail",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "GivtName",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "GivtPhoneNumber",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "GivtWebsite",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Locale",
                table: "Countries");
        }
    }
}
