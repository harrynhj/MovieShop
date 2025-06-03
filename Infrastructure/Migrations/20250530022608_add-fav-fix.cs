using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addfavfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUsers_Movies_MoviesId",
                table: "MoviesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUsers_Users_FavoritesId",
                table: "MoviesUsers");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "MoviesUsers",
                newName: "FavoritesId1");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesUsers_MoviesId",
                table: "MoviesUsers",
                newName: "IX_MoviesUsers_FavoritesId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUsers_Movies_FavoritesId",
                table: "MoviesUsers",
                column: "FavoritesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUsers_Users_FavoritesId1",
                table: "MoviesUsers",
                column: "FavoritesId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUsers_Movies_FavoritesId",
                table: "MoviesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUsers_Users_FavoritesId1",
                table: "MoviesUsers");

            migrationBuilder.RenameColumn(
                name: "FavoritesId1",
                table: "MoviesUsers",
                newName: "MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesUsers_FavoritesId1",
                table: "MoviesUsers",
                newName: "IX_MoviesUsers_MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUsers_Movies_MoviesId",
                table: "MoviesUsers",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUsers_Users_FavoritesId",
                table: "MoviesUsers",
                column: "FavoritesId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
