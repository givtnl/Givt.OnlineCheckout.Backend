using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataMedium_Merchants_MerchantId",
                table: "DataMedium");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataMedium",
                table: "DataMedium");

            migrationBuilder.RenameTable(
                name: "DataMedium",
                newName: "Mediums");

            migrationBuilder.RenameIndex(
                name: "IX_DataMedium_MerchantId",
                table: "Mediums",
                newName: "IX_Mediums_MerchantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mediums",
                table: "Mediums",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mediums_Merchants_MerchantId",
                table: "Mediums",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mediums_Merchants_MerchantId",
                table: "Mediums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mediums",
                table: "Mediums");

            migrationBuilder.RenameTable(
                name: "Mediums",
                newName: "DataMedium");

            migrationBuilder.RenameIndex(
                name: "IX_Mediums_MerchantId",
                table: "DataMedium",
                newName: "IX_DataMedium_MerchantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataMedium",
                table: "DataMedium",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataMedium_Merchants_MerchantId",
                table: "DataMedium",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
