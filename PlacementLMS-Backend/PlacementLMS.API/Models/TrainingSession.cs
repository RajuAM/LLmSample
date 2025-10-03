using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class TrainingSession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Institution")]
        public int InstitutionId { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        [StringLength(200)]
        public string Venue { get; set; }

        public string InstructorName { get; set; }

        public int MaxParticipants { get; set; }

        public int CurrentParticipants { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual Institution Institution { get; set; }

        // Collections
        public virtual ICollection<SessionRegistration> SessionRegistrations { get; set; }
    }
}