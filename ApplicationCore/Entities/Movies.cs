using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("Movies")]
public class Movie
{
    public int Id { get; set; }

    [MaxLength(2084)]
    public string? BackdropUrl { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal? Budget { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    [MaxLength(2084)]
    public string? ImdbUrl { get; set; }

    [MaxLength(64)]
    public string? OriginalLanguage { get; set; }

    public string? Overview { get; set; }

    [MaxLength(2084)]
    public string? PosterUrl { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? Price { get; set; }

    public DateTime? ReleaseDate { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal? Revenue { get; set; }

    public int? RunTime { get; set; }

    [MaxLength(512)]
    public string? Tagline { get; set; }

    [MaxLength(256)]
    public string? Title { get; set; }

    [MaxLength(2084)]
    public string? TmdbUrl { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
    
    public ICollection<MovieGenres> MovieGenres { get; set; }
    public ICollection<MovieCasts> MovieCasts { get; set; }
}