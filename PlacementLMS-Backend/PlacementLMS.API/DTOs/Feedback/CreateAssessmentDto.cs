using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Feedback
{
    public class CreateAssessmentDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string AssessmentType { get; set; }

        [Required]
        [StringLength(50)]
        public string TargetAudience { get; set; }

        [Range(1, 100)]
        public int TotalPoints { get; set; } = 100;

        [Range(1, 24)]
        public int TimeLimitHours { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<CreateAssessmentQuestionDto> Questions { get; set; }
    }

    public class CreateAssessmentQuestionDto
    {
        [Required]
        public string QuestionText { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionType { get; set; }

        [Range(1, 100)]
        public int Points { get; set; } = 1;

        [Range(1, 50)]
        public int Order { get; set; } = 1;

        public bool IsRequired { get; set; } = true;

        public string Options { get; set; }

        public string CorrectAnswer { get; set; }
    }
}