using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Industry { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Website { get; set; }

        [StringLength(100)]
        public string CompanySize { get; set; } // Startup, Small, Medium, Large, Enterprise

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        [StringLength(100)]
        public string ContactEmail { get; set; }

        [StringLength(100)]
        public string HRContactName { get; set; }

        [StringLength(20)]
        public string HRContactNumber { get; set; }

        [StringLength(100)]
        public string HRContactEmail { get; set; }

        public string LogoPath { get; set; }

        public bool IsVerified { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovalDate { get; set; }

        // Navigation properties
        public virtual ICollection<JobOpportunity> JobOpportunities { get; set; }
        public virtual ICollection<Feedback> ReceivedFeedback { get; set; }
    }
}