using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class AddApplicationFeeAndChangeCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Organisations");

            migrationBuilder.AddColumn<decimal>(
                name: "ApplicationFeeFixedAmount",
                table: "Countries",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ApplicationFeePercentage",
                table: "Countries",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Countries",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationFeeFixedAmount",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ApplicationFeePercentage",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Countries");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Organisations",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);
        }
    }
}
