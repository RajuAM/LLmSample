using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class StudentAssessmentAnswer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("StudentAssessment")]
        public int StudentAssessmentId { get; set; }

        [Required]
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public string AnswerText { get; set; }

        public int? Rating { get; set; } // For rating type questions

        public bool? YesNoAnswer { get; set; } // For yes/no questions

        [Range(0, 100)]
        public decimal PointsEarned { get; set; }

        public bool IsCorrect { get; set; } = false;

        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual StudentAssessment StudentAssessment { get; set; }
        public virtual AssessmentQuestion Question { get; set; }
    }
}