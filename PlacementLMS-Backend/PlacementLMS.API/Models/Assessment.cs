using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Assessment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("CreatedBy")]
        public int CreatedByUserId { get; set; }

        [Required]
        [StringLength(50)]
        public string AssessmentType { get; set; } // Technical, Behavioral, Communication, Leadership, etc.

        [Required]
        [StringLength(50)]
        public string TargetAudience { get; set; } // Students, Recruiters, Instructors, Companies

        [Range(1, 100)]
        public int TotalPoints { get; set; } = 100;

        [Range(1, 24)]
        public int TimeLimitHours { get; set; } // Assessment time limit

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsTemplate { get; set; } = false; // Can be reused

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual User CreatedBy { get; set; }

        // Collections
        public virtual ICollection<AssessmentQuestion> Questions { get; set; }
        public virtual ICollection<StudentAssessment> StudentAssessments { get; set; }
    }
}