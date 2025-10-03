using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

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
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public string UserType { get; set; } // Admin, Student, Institution, Industry

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        // Collections
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Feedback> GivenFeedback { get; set; }
        public virtual ICollection<Feedback> ReceivedFeedback { get; set; }
        public virtual ICollection<Assessment> CreatedAssessments { get; set; }
    }
}