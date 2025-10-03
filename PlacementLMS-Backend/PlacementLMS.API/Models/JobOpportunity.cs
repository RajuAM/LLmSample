using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class JobOpportunity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Industry")]
        public int IndustryId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string JobType { get; set; } // Full-time, Part-time, Internship

        [Required]
        [StringLength(100)]
        public string ExperienceLevel { get; set; } // Entry, Mid, Senior

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        public decimal SalaryMin { get; set; }

        public decimal SalaryMax { get; set; }

        [Required]
        public string SkillsRequired { get; set; }

        public string Responsibilities { get; set; }

        public string Benefits { get; set; }

        [Required]
        public int NumberOfPositions { get; set; }

        public int NumberOfApplications { get; set; } = 0;

        public DateTime ApplicationDeadline { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Industry Industry { get; set; }
        public virtual Company Company { get; set; }

        // Collections
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}