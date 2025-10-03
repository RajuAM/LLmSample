using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Industry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyRegistrationNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string State { get; set; }

        [Required]
        [StringLength(20)]
        public string Pincode { get; set; }

        [Required]
        [StringLength(15)]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        public string Website { get; set; }

        public string LogoFilePath { get; set; }

        [Required]
        [StringLength(200)]
        public string IndustryType { get; set; } // IT, Finance, Healthcare, etc.

        public int EmployeeCount { get; set; }

        public bool IsVerified { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime EstablishedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual User User { get; set; }

        // Collections
        public virtual ICollection<JobOpportunity> JobOpportunities { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}