using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("MovieCasts")]
public class MovieCasts
{
    public int CastId { get; set; }
    [MaxLength(450)]
    public string Character { get; set; }
    public int MovieId { get; set; }
    
    
    public Casts Casts { get; set; }
    public Movie Movie { get; set; }

}