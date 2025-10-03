using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Feedback
{
    public class UpdateFeedbackDto
    {
        [Required]
        [StringLength(20)]
        public string Category { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comments { get; set; }

        [StringLength(1000)]
        public string Strengths { get; set; }

        [StringLength(1000)]
        public string AreasForImprovement { get; set; }

        public bool IsPublished { get; set; }
    }
}