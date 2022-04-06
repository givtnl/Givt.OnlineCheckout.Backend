﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Givt.OnlineCheckout.Infrastructure.Migrations
{
    public partial class AddImageLogoLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoImageLink",
                table: "Organisations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoImageLink",
                table: "Organisations");
        }
    }
}
