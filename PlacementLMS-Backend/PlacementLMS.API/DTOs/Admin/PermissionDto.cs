using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Admin
{
    public class PermissionDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Module { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; }
    }

    public class PermissionResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoleCount { get; set; }
    }

    public class RolePermissionAssignmentDto
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public List<int> PermissionIds { get; set; } = new List<int>();
    }
}