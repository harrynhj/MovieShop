using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class MovieShopDbContext : DbContext
{
    
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cast> Casts { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
    {
        
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenres);
        modelBuilder.Entity<MovieCast>(ConfigureMovieCasts);
        modelBuilder.Entity<Review>(ConfigureReviews);
        modelBuilder.Entity<Purchase>(ConfigurePurchase);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Movie)
            .WithMany(m => m.User)
            .UsingEntity(j => j.ToTable("Favorites"));
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Role)
            .WithMany(r => r.User)
            .UsingEntity(j => j.ToTable("UserRoles"));
        
        modelBuilder.Entity<Movie>()
            .Property(m => m.Price).HasColumnType("decimal(5,2)");
        modelBuilder.Entity<Movie>()
            .Property(m => m.Budget).HasColumnType("decimal(18,4)");
        modelBuilder.Entity<Movie>()
            .Property(m => m.Revenue).HasColumnType("decimal(18,4)");
    }

    private void ConfigureMovieCasts(EntityTypeBuilder<MovieCast> builder)
    {
        builder.HasKey(e => new {e.MovieId, e.CastId});
        builder.HasOne(e => e.Movie)
            .WithMany(e => e.MovieCast)
            .HasForeignKey(e => e.MovieId);
        builder.HasOne(e => e.Cast)
            .WithMany(e => e.MovieCast)
            .HasForeignKey(e => e.CastId);
    }

    private void ConfigureMovieGenres(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.HasKey(k => new { k.GenreId, k.MovieId });
        builder.HasOne(e => e.Movie)
            .WithMany(e => e.MovieGenre)
            .HasForeignKey(e => e.MovieId);
        builder.HasOne(e => e.Genre)
            .WithMany(e => e.MovieGenre)
            .HasForeignKey(e => e.GenreId);
    }

    private void ConfigureReviews(EntityTypeBuilder<Review> builder)
    {
        builder.Property(p => p.Rating).HasColumnType("decimal(3,2)");
        builder.HasKey(k => new { k.UserId, k.MovieId });
        builder.HasOne(e => e.User)
            .WithMany(e => e.Review)
            .HasForeignKey(e => e.UserId);
        builder.HasOne(e => e.Movie)
            .WithMany(e => e.Review)
            .HasForeignKey(e => e.MovieId);
    }

    private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
    {
        builder.Property(p =>p.TotalPrice).HasColumnType("decimal(5,2)");
        builder.HasKey(k => new { k.MovieId, k.UserId});
        builder.HasOne(e => e.User)
            .WithMany(e => e.Purchase)
            .HasForeignKey(e => e.UserId);
        builder.HasOne(e => e.Movie)
            .WithMany(e => e.Purchase)
            .HasForeignKey(e => e.MovieId);
    }


}