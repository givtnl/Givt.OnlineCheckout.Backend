using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class RenameCountryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_CountryData_CountryId",
                table: "Organisations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryData",
                table: "CountryData");

            migrationBuilder.RenameTable(
                name: "CountryData",
                newName: "Countries");

            migrationBuilder.RenameIndex(
                name: "IX_CountryData_CountryCode",
                table: "Countries",
                newName: "IX_Countries_CountryCode");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaymentMethods",
                table: "Organisations",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaymentMethods",
                table: "Countries",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_Countries_CountryId",
                table: "Organisations",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_Countries_CountryId",
                table: "Organisations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "CountryData");

            migrationBuilder.RenameIndex(
                name: "IX_Countries_CountryCode",
                table: "CountryData",
                newName: "IX_CountryData_CountryCode");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethods",
                table: "Organisations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethods",
                table: "CountryData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryData",
                table: "CountryData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_CountryData_CountryId",
                table: "Organisations",
                column: "CountryId",
                principalTable: "CountryData",
                principalColumn: "Id");
        }
    }
}
