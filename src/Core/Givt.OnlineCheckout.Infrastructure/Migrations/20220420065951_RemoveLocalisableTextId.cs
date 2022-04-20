using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class RemoveLocalisableTextId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrganisationTexts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MediumTexts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "OrganisationTexts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "MediumTexts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
