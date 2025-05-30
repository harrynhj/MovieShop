using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mgrefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenresId",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_MoviesId",
                table: "MovieGenres");

            migrationBuilder.DropTable(
                name: "MovieCasts");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "MovieGenres",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "MovieGenres",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenres_MoviesId",
                table: "MovieGenres",
                newName: "IX_MovieGenres_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_GenreId",
                table: "MovieGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movies_MovieId",
                table: "MovieGenres",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenreId",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_MovieId",
                table: "MovieGenres");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MovieGenres",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "MovieGenres",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenres_MovieId",
                table: "MovieGenres",
                newName: "IX_MovieGenres_MoviesId");

            migrationBuilder.CreateTable(
                name: "MovieCasts",
                columns: table => new
                {
                    CastsId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCasts", x => new { x.CastsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_MovieCasts_Casts_CastsId",
                        column: x => x.CastsId,
                        principalTable: "Casts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCasts_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCasts_MoviesId",
                table: "MovieCasts",
                column: "MoviesId");

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
    }
}
