using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class MovieShopDbContext : DbContext
{
    
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Casts> Casts { get; set; }
    public DbSet<MovieGenres> MovieGenres { get; set; }
    public DbSet<MovieCasts> MovieCasts { get; set; }
    
    
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
    {
        
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MovieGenres>(ConfigureMovieGenres);
        modelBuilder.Entity<MovieCasts>(ConfigureMovieCasts);
    }

    private void ConfigureMovieCasts(EntityTypeBuilder<MovieCasts> builder)
    {
        builder.HasKey(e => new {e.MovieId, e.CastId});
        builder.HasOne(e => e.Movie)
            .WithMany(e => e.MovieCasts)
            .HasForeignKey(e => e.MovieId);
        builder.HasOne(e => e.Casts)
            .WithMany(e => e.MovieCasts)
            .HasForeignKey(e => e.CastId);
    }

    private void ConfigureMovieGenres(EntityTypeBuilder<MovieGenres> builder)
    {
        builder.HasKey(k => new { k.GenreId, k.MovieId });
        builder.HasOne(e => e.Movie)
            .WithMany(e => e.MovieGenres)
            .HasForeignKey(e => e.MovieId);
        builder.HasOne(e => e.Genre)
            .WithMany(e => e.MovieGenres)
            .HasForeignKey(e => e.GenreId);
    }


}