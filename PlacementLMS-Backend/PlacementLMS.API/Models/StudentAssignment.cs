using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class StudentAssignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }

        [Required]
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public string SubmissionFilePath { get; set; }

        public string SubmissionText { get; set; }

        public int? PointsScored { get; set; }

        public string Feedback { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Submitted"; // Submitted, Graded, Returned

        public DateTime? GradedAt { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}