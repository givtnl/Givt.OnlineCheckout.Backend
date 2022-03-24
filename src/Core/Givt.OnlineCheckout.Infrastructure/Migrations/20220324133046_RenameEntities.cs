using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class RenameEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Customers_DonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Mediums_Merchants_OrganisationId",
                table: "Mediums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Merchants",
                newName: "Organisations");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Donors");

            migrationBuilder.RenameIndex(
                name: "IX_Merchants_Namespace",
                table: "Organisations",
                newName: "IX_Organisations_Namespace");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Email",
                table: "Donors",
                newName: "IX_Donors_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organisations",
                table: "Organisations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Donors",
                table: "Donors",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organisations",
                table: "Organisations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Donors",
                table: "Donors");

            migrationBuilder.RenameTable(
                name: "Organisations",
                newName: "Merchants");

            migrationBuilder.RenameTable(
                name: "Donors",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Organisations_Namespace",
                table: "Merchants",
                newName: "IX_Merchants_Namespace");

            migrationBuilder.RenameIndex(
                name: "IX_Donors_Email",
                table: "Customers",
                newName: "IX_Customers_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Customers_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mediums_Merchants_OrganisationId",
                table: "Mediums",
                column: "OrganisationId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
