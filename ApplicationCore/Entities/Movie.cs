using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("Movies")]
public class Movie
{
    public int Id { get; set; }
    [MaxLength(2084)]
    public string? BackdropUrl { get; set; }
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
    public decimal? Price { get; set; }
    public DateTime? ReleaseDate { get; set; }
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
    
    
    public ICollection<MovieGenre> MovieGenre { get; set; }
    public ICollection<MovieCast> MovieCast { get; set; }
    public ICollection<User> User{ get; set; }
    public ICollection<Review> Review { get; set; }
    public ICollection<Purchase> Purchase { get; set; }
}