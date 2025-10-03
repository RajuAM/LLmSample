using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string CertificateNumber { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Institution")]
        public int InstitutionId { get; set; }

        [Required]
        [StringLength(200)]
        public string CourseName { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string CertificateFilePath { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual Institution Institution { get; set; }
    }
}