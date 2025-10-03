using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Module { get; set; } // UserManagement, CourseManagement, PlacementManagement, etc.

        [Required]
        [StringLength(50)]
        public string Action { get; set; } // Create, Read, Update, Delete, Approve, etc.

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Collections
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}