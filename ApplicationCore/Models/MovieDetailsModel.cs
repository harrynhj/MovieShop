using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class MovieDetailsModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? BackDropUrl { get; set; }
    public string? ImdbUrl  { get; set; }
    public string? TmdbUrl { get; set; }
    public string? OriginalLanguage  { get; set; }
    public decimal? Price { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public decimal? Rating { get; set; }
    public int? Runtime { get; set; }
    public string? PosterURL { get; set; }
    public string? Overview { get; set; } 
    public string? TagLine { get; set; }
    public decimal? Budget { get; set; }
    public decimal? Revenue { get; set; }
    public List<string>? Genres { get; set; }
}