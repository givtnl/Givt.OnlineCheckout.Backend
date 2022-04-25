using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class CountryCodeIsKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_Countries_CountryId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_CountryId",
                table: "Organisations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_CountryCode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Countries");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Organisations",
                type: "character varying(2)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Countries",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_CountryCode",
                table: "Organisations",
                column: "CountryCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_Countries_CountryCode",
                table: "Organisations",
                column: "CountryCode",
                principalTable: "Countries",
                principalColumn: "CountryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_Countries_CountryCode",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_CountryCode",
                table: "Organisations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Organisations");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Organisations",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Countries",
                type: "character varying(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldMaxLength: 2);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Countries",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_CountryId",
                table: "Organisations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryCode",
                table: "Countries",
                column: "CountryCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_Countries_CountryId",
                table: "Organisations",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
