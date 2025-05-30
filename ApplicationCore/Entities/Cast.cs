using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;
[Table("Casts")]
public class Cast
{
    public int Id { get; set; }
    public String Gender { get; set; }
    [MaxLength(128)]
    public String Name { get; set; }
    [MaxLength(2084)]
    public String ProfilePath { get; set; }
    public String TmdbUrl { get; set; }
    
    public ICollection<MovieCast> MovieCast { get; set; }
}