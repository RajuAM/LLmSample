using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class SessionRegistration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("TrainingSession")]
        public int TrainingSessionId { get; set; }

        [Required]
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string Status { get; set; } = "Registered"; // Registered, Attended, Cancelled

        public DateTime? AttendedAt { get; set; }

        public string Notes { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual TrainingSession TrainingSession { get; set; }
    }
}