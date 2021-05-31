using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ValetParking.Persistence.Migrations
{
    public partial class RemovedHashAndSaltFromPasswordRecovery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "PasswordRecovery");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "PasswordRecovery");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Hash",
                table: "PasswordRecovery",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "PasswordRecovery",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
