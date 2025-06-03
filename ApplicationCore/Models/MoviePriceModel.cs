using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class MoviePriceModel
{
    [Required]
    public int MovieId { get; set; }

    [Required]
    [Range(0, 999.99, ErrorMessage = "Price must be between 0 and 999.99")]
    public decimal Price { get; set; }
}