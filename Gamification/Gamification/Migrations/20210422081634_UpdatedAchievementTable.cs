using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamification.Migrations
{
    public partial class UpdatedAchievementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AchievementUser_Users_AchievementsId1",
                table: "AchievementUser");

            migrationBuilder.RenameColumn(
                name: "AchievementsId1",
                table: "AchievementUser",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_AchievementUser_AchievementsId1",
                table: "AchievementUser",
                newName: "IX_AchievementUser_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AchievementUser_Users_UsersId",
                table: "AchievementUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AchievementUser_Users_UsersId",
                table: "AchievementUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "AchievementUser",
                newName: "AchievementsId1");

            migrationBuilder.RenameIndex(
                name: "IX_AchievementUser_UsersId",
                table: "AchievementUser",
                newName: "IX_AchievementUser_AchievementsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AchievementUser_Users_AchievementsId1",
                table: "AchievementUser",
                column: "AchievementsId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
