using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class junctionmgrename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genres_GenresId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Movies_MoviesId",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie");

            migrationBuilder.RenameTable(
                name: "GenreMovie",
                newName: "MovieGenres");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "MovieGenres",
                newName: "IX_MovieGenres_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres",
                columns: new[] { "GenresId", "MoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_GenresId",
                table: "MovieGenres",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movies_MoviesId",
                table: "MovieGenres",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenresId",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_MoviesId",
                table: "MovieGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres");

            migrationBuilder.RenameTable(
                name: "MovieGenres",
                newName: "GenreMovie");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenres_MoviesId",
                table: "GenreMovie",
                newName: "IX_GenreMovie_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie",
                columns: new[] { "GenresId", "MoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genres_GenresId",
                table: "GenreMovie",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Movies_MoviesId",
                table: "GenreMovie",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
