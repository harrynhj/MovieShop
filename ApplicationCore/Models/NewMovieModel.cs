using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class NewMovieModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(2084)]
        [Display(Name = "Backdrop URL")]
        public string? BackdropUrl { get; set; }

        [Required]
        [Range(0, 999999999999.9999)]
        public decimal? Budget { get; set; }
        
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [Required]
        [MaxLength(2084)]
        [Display(Name = "IMDb URL")]
        public string? ImdbUrl { get; set; }
        
        [MaxLength(64)]
        [Display(Name = "Original Language")]
        public string? OriginalLanguage { get; set; }
        
        [Required]
        public string? Overview { get; set; }

        [Required]
        [MaxLength(2084)]
        [Display(Name = "Poster URL")]
        public string? PosterUrl { get; set; }

        [Required]
        [Range(0, 9999.99)]
        public decimal? Price { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(0, 999999999999.9999)]
        public decimal? Revenue { get; set; }

        [Required]
        [Display(Name = "Runtime (minutes)")]
        [Range(0, 1000)]
        public int? RunTime { get; set; }

        [Required]
        [MaxLength(512)]
        public string? Tagline { get; set; }

        [Required]
        [MaxLength(256)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(2084)]
        [Display(Name = "TMDb URL")]
        public string? TmdbUrl { get; set; }

        [Display(Name = "Updated By")]
        public string? UpdatedBy { get; set; }

        [Display(Name = "Updated Date")]
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }
    }
}