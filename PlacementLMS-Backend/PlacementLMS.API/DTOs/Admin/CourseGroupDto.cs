using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Admin
{
    public class CourseGroupDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class CourseGroupResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CourseCount { get; set; }
    }
}