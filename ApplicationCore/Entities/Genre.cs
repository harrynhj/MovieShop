﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("Genres")]
public class Genre
{
    public int Id { get; set; }
    [MaxLength(24)]
    public string Name { get; set; }
    
    public ICollection<MovieGenre> MovieGenre { get; set; }
}