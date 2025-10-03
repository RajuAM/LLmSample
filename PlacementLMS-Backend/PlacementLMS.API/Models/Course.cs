using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Course
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
        [StringLength(100)]
        public string Category { get; set; } // Technical, Soft Skills, Domain-specific

        [ForeignKey("CourseGroup")]
        public int? CourseGroupId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal DiscountPrice { get; set; }

        [Required]
        public int DurationHours { get; set; }

        public string ThumbnailImagePath { get; set; }

        public string VideoPath { get; set; }

        public string PDFMaterialPath { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsApproved { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        // Navigation properties
        public virtual Institution Institution { get; set; }
        public virtual CourseGroup CourseGroup { get; set; }

        // Collections
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Feedback> CourseFeedback { get; set; }
    }
}