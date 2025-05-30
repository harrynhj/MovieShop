using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("MovieGenres")]
public class MovieGenres
{
    public int GenreId { get; set; }
    public int MovieId { get; set; }
    
    
    public Genre Genre { get; set; }
    public Movie Movie { get; set; }

}