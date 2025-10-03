using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Institution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string InstitutionName { get; set; }

        [Required]
        [StringLength(100)]
        public string InstitutionCode { get; set; }

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

        public bool IsVerified { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime EstablishedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual User User { get; set; }

        // Collections
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<MockTest> MockTests { get; set; }
        public virtual ICollection<TrainingSession> TrainingSessions { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
    }
}