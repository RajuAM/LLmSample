using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Admin
{
    public class UserManagementDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string UserType { get; set; } // Admin, Student, Institution, Industry

        public List<int> RoleIds { get; set; } = new List<int>();
    }

    public class UserResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string DepartmentName { get; set; }
        public string InstitutionName { get; set; }
    }

    public class UserRoleAssignmentDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}