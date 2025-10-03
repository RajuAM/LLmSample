using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Admin
{
    public class DepartmentDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        public int? InstitutionId { get; set; }
    }

    public class DepartmentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int? InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StudentCount { get; set; }
    }
}