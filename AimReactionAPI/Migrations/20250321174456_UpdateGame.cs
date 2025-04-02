using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AimReactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Visibility",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CreatorId",
                table: "Games",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_CreatorId",
                table: "Games",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_CreatorId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_CreatorId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "Games");
        }
    }
}
