using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("FromUser")]
        public int FromUserId { get; set; } // Who is giving the feedback

        [Required]
        [ForeignKey("ToUser")]
        public int ToUserId { get; set; } // Who is receiving the feedback (optional for content feedback)

        [ForeignKey("Student")]
        public int? StudentId { get; set; } // For recruiter feedback on students

        [ForeignKey("Company")]
        public int? CompanyId { get; set; } // For student feedback on companies

        [ForeignKey("Course")]
        public int? CourseId { get; set; } // For course/training feedback

        [ForeignKey("JobApplication")]
        public int? JobApplicationId { get; set; } // For interview feedback

        [ForeignKey("TrainingSession")]
        public int? TrainingSessionId { get; set; } // For training session feedback

        [Required]
        [StringLength(50)]
        public string FeedbackType { get; set; } // RecruiterToStudent, StudentToProcess, ContentFeedback, TrainingFeedback

        [Required]
        [StringLength(20)]
        public string Category { get; set; } // Technical, Communication, Process, Content, Overall

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; } // 1-5 star rating

        [Required]
        public string Comments { get; set; }

        [StringLength(1000)]
        public string Strengths { get; set; } // What went well

        [StringLength(1000)]
        public string AreasForImprovement { get; set; } // Areas that need improvement

        public bool IsAnonymous { get; set; } = false;

        public bool IsPublished { get; set; } = false; // For public display

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
        public virtual Student Student { get; set; }
        public virtual Company Company { get; set; }
        public virtual Course Course { get; set; }
        public virtual JobApplication JobApplication { get; set; }
        public virtual TrainingSession TrainingSession { get; set; }
    }
}