using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("MovieCasts")]
public class MovieCast
{
    public int CastId { get; set; }
    [MaxLength(450)]
    public string Character { get; set; }
    public int MovieId { get; set; }
    
    
    public Cast Cast { get; set; }
    public Movie Movie { get; set; }

}