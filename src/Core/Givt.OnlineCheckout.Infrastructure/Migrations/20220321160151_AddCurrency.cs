using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class AddCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Merchants",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Merchants");
        }
    }
}
