using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class RenameEntityColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Customers_DataCustomerId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Mediums_Merchants_MerchantId",
                table: "Mediums");

            migrationBuilder.DropColumn(
                name: "PaymentProviderTransactionReference",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "MerchantId",
                table: "Mediums",
                newName: "OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_Mediums_MerchantId",
                table: "Mediums",
                newName: "IX_Mediums_OrganisationId");

            migrationBuilder.RenameColumn(
                name: "DataCustomerId",
                table: "Donations",
                newName: "DonorId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_DataCustomerId",
                table: "Donations",
                newName: "IX_Donations_DonorId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentProviderAccountReference",
                table: "Merchants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Namespace",
                table: "Merchants",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Merchants",
                type: "character varying(35)",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Merchants",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "ThankYou",
                table: "Mediums",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Medium",
                table: "Mediums",
                type: "character varying(33)",
                maxLength: 33,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Goal",
                table: "Mediums",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Amounts",
                table: "Mediums",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Donations",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Donations",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionReference",
                table: "Donations",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "character varying(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_Namespace",
                table: "Merchants",
                column: "Namespace");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Customers_DonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Mediums_Merchants_OrganisationId",
                table: "Mediums");

            migrationBuilder.DropIndex(
                name: "IX_Merchants_Namespace",
                table: "Merchants");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "TransactionReference",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "OrganisationId",
                table: "Mediums",
                newName: "MerchantId");

            migrationBuilder.RenameIndex(
                name: "IX_Mediums_OrganisationId",
                table: "Mediums",
                newName: "IX_Mediums_MerchantId");

            migrationBuilder.RenameColumn(
                name: "DonorId",
                table: "Donations",
                newName: "DataCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_DonorId",
                table: "Donations",
                newName: "IX_Donations_DataCustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentProviderAccountReference",
                table: "Merchants",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Namespace",
                table: "Merchants",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Merchants",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(35)",
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Currency",
                table: "Merchants",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "character varying(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThankYou",
                table: "Mediums",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Medium",
                table: "Mediums",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(33)",
                oldMaxLength: 33,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Goal",
                table: "Mediums",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Amounts",
                table: "Mediums",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentProviderTransactionReference",
                table: "Donations",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Customers_DataCustomerId",
                table: "Donations",
                column: "DataCustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mediums_Merchants_MerchantId",
                table: "Mediums",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
