using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class RemoveGoalThankYouFromMedium : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Mediums");

            migrationBuilder.DropColumn(
                name: "ThankYou",
                table: "Mediums");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "Mediums",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThankYou",
                table: "Mediums",
                type: "text",
                nullable: true);
        }
    }
}
