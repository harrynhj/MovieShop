using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmoviefix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BackdropUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImdbUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false),
                    OriginalLanguage = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Revenue = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    RunTime = table.Column<int>(type: "int", nullable: true),
                    Tagline = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TmdbUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
