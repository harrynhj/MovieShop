using System.ComponentModel.DataAnnotations;


namespace ApplicationCore.Models;

public class ReviewModel
{
    [Required(ErrorMessage = "Score is required")]
    [Range(0, 10, ErrorMessage = "Score must be between 0 and 10")]
    public int Score { get; set; }

    [Required(ErrorMessage = "Review text is required")]
    [StringLength(1000, ErrorMessage = "Review cannot exceed 1000 characters")]
    public string Review { get; set; }
    
    public int MovieId { get; set; }

    public int UserId { get; set; }
}