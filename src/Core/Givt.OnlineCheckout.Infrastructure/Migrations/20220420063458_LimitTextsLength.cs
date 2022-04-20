using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class LimitTextsLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationTexts",
                table: "OrganisationTexts");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationTexts_OrganisationId",
                table: "OrganisationTexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediumTexts",
                table: "MediumTexts");

            migrationBuilder.DropIndex(
                name: "IX_MediumTexts_MediumId",
                table: "MediumTexts");

            migrationBuilder.AlterColumn<string>(
                name: "ThankYou",
                table: "OrganisationTexts",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OrganisationId",
                table: "OrganisationTexts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LanguageId",
                table: "OrganisationTexts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Goal",
                table: "OrganisationTexts",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "OrganisationTexts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ThankYou",
                table: "MediumTexts",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MediumId",
                table: "MediumTexts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LanguageId",
                table: "MediumTexts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Goal",
                table: "MediumTexts",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "MediumTexts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationTexts",
                table: "OrganisationTexts",
                columns: new[] { "OrganisationId", "LanguageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediumTexts",
                table: "MediumTexts",
                columns: new[] { "MediumId", "LanguageId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationTexts",
                table: "OrganisationTexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediumTexts",
                table: "MediumTexts");

            migrationBuilder.AlterColumn<string>(
                name: "ThankYou",
                table: "OrganisationTexts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OrganisationTexts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Goal",
                table: "OrganisationTexts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LanguageId",
                table: "OrganisationTexts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "OrganisationId",
                table: "OrganisationTexts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "ThankYou",
                table: "MediumTexts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MediumTexts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Goal",
                table: "MediumTexts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LanguageId",
                table: "MediumTexts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "MediumId",
                table: "MediumTexts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationTexts",
                table: "OrganisationTexts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediumTexts",
                table: "MediumTexts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationTexts_OrganisationId",
                table: "OrganisationTexts",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_MediumTexts_MediumId",
                table: "MediumTexts",
                column: "MediumId");
        }
    }
}
