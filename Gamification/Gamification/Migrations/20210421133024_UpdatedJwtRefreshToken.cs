using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamification.Migrations
{
    public partial class UpdatedJwtRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Revoked",
                table: "JwtRefreshToken",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revoked",
                table: "JwtRefreshToken");
        }
    }
}
