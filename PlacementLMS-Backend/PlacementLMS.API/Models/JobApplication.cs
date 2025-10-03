using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("JobOpportunity")]
        public int JobOpportunityId { get; set; }

        [Required]
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string Status { get; set; } = "Applied"; // Applied, Shortlisted, Interviewed, Selected, Rejected

        public string CoverLetter { get; set; }

        public DateTime? InterviewDate { get; set; }

        public string InterviewFeedback { get; set; }

        public bool IsSelected { get; set; } = false;

        public DateTime? SelectionDate { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual JobOpportunity JobOpportunity { get; set; }
    }
}