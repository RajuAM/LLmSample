using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Feedback
{
    public class CreateFeedbackDto
    {
        [Required]
        public int ToUserId { get; set; }

        public int? StudentId { get; set; }

        public int? CompanyId { get; set; }

        public int? CourseId { get; set; }

        public int? JobApplicationId { get; set; }

        public int? TrainingSessionId { get; set; }

        [Required]
        [StringLength(50)]
        public string FeedbackType { get; set; }

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

        public bool IsAnonymous { get; set; } = false;
    }
}