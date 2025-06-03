using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addfavfix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUsers_Movies_FavoritesId",
                table: "MoviesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUsers_Users_FavoritesId1",
                table: "MoviesUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviesUsers",
                table: "MoviesUsers");

            migrationBuilder.RenameTable(
                name: "MoviesUsers",
                newName: "Favorites");

            migrationBuilder.RenameColumn(
                name: "FavoritesId1",
                table: "Favorites",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "FavoritesId",
                table: "Favorites",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesUsers_FavoritesId1",
                table: "Favorites",
                newName: "IX_Favorites_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                columns: new[] { "MovieId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Movies_MovieId",
                table: "Favorites",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Movies_MovieId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "MoviesUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MoviesUsers",
                newName: "FavoritesId1");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MoviesUsers",
                newName: "FavoritesId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_UserId",
                table: "MoviesUsers",
                newName: "IX_MoviesUsers_FavoritesId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviesUsers",
                table: "MoviesUsers",
                columns: new[] { "FavoritesId", "FavoritesId1" });

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
    }
}
