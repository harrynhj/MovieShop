using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Casts
{
    public int Id { get; set; }
    public String Gender { get; set; }
    [MaxLength(128)]
    public String Name { get; set; }
    [MaxLength(2084)]
    public String ProfilePath { get; set; }
    public String TmdbUrl { get; set; }
    
    public ICollection<MovieCasts> MovieCasts { get; set; }
}