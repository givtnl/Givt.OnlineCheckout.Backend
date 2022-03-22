using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class ChangeAmountsToCommaSeparatedValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Amounts",
                table: "DataMedium",
                type: "text",
                nullable: false,
                oldClrType: typeof(decimal[]),
                oldType: "numeric[]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal[]>(
                name: "Amounts",
                table: "DataMedium",
                type: "numeric[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
