using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int MaxPoints { get; set; }

        public string Instructions { get; set; }

        public string AttachmentPath { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual Course Course { get; set; }

        // Collections
        public virtual ICollection<StudentAssignment> StudentAssignments { get; set; }
    }
}