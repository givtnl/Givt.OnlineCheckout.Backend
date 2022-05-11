using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class RemoveOrganisationTexts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganisationTexts");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MediumTexts",
                type: "character varying(175)",
                maxLength: 175,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "MediumTexts");

            migrationBuilder.CreateTable(
                name: "OrganisationTexts",
                columns: table => new
                {
                    OrganisationId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Goal = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    ThankYou = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationTexts", x => new { x.OrganisationId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_OrganisationTexts_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
