using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class AssessmentQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Assessment")]
        public int AssessmentId { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionType { get; set; } // MultipleChoice, Rating, Text, YesNo

        [Range(1, 100)]
        public int Points { get; set; } = 1;

        [Range(1, 50)]
        public int Order { get; set; } = 1;

        public bool IsRequired { get; set; } = true;

        public string Options { get; set; } // JSON string for multiple choice options

        public string CorrectAnswer { get; set; } // For multiple choice questions

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Assessment Assessment { get; set; }

        // Collections
        public virtual ICollection<StudentAssessmentAnswer> StudentAnswers { get; set; }
    }
}