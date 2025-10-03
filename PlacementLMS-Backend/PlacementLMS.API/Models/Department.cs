using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [ForeignKey("Institution")]
        public int? InstitutionId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Institution Institution { get; set; }

        // Collections
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<CourseGroup> CourseGroups { get; set; }
    }
}