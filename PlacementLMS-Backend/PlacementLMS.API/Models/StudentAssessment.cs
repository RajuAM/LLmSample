using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class StudentAssessment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Assessment")]
        public int AssessmentId { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        [Range(0, 100)]
        public decimal Score { get; set; }

        [Range(0, 100)]
        public decimal Percentage { get; set; }

        public bool IsCompleted { get; set; } = false;

        public bool IsPassed { get; set; } = false;

        public string Status { get; set; } = "NotStarted"; // NotStarted, InProgress, Completed, Overdue

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual Assessment Assessment { get; set; }

        // Collections
        public virtual ICollection<StudentAssessmentAnswer> Answers { get; set; }
    }
}