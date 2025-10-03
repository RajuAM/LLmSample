using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Resume
{
    public class ResumeDto
    {
        [Required]
        public int StudentId { get; set; }

        [StringLength(500)]
        public string Objective { get; set; }

        [StringLength(1000)]
        public string Skills { get; set; }

        public List<EducationDto> Education { get; set; } = new List<EducationDto>();

        public List<ExperienceDto> Experience { get; set; } = new List<ExperienceDto>();

        public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();

        public List<CertificationDto> Certifications { get; set; } = new List<CertificationDto>();

        public string ResumeFilePath { get; set; }

        public bool IsPublic { get; set; } = false;

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }

    public class EducationDto
    {
        [Required]
        [StringLength(200)]
        public string Degree { get; set; }

        [Required]
        [StringLength(200)]
        public string Institution { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Range(0.0, 10.0)]
        public double? GPA { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }

    public class ExperienceDto
    {
        [Required]
        [StringLength(200)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(200)]
        public string Company { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public bool IsCurrentJob { get; set; } = false;
    }

    public class ProjectDto
    {
        [Required]
        [StringLength(200)]
        public string ProjectName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Technologies { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(500)]
        public string ProjectUrl { get; set; }
    }

    public class CertificationDto
    {
        [Required]
        [StringLength(200)]
        public string CertificationName { get; set; }

        [Required]
        [StringLength(200)]
        public string IssuingOrganization { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(200)]
        public string CredentialId { get; set; }

        [StringLength(500)]
        public string CredentialUrl { get; set; }
    }

    public class ResumeResponseDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Objective { get; set; }
        public string Skills { get; set; }
        public List<EducationDto> Education { get; set; }
        public List<ExperienceDto> Experience { get; set; }
        public List<ProjectDto> Projects { get; set; }
        public List<CertificationDto> Certifications { get; set; }
        public string ResumeFilePath { get; set; }
        public bool IsPublic { get; set; }
        public DateTime LastUpdated { get; set; }
        public double? ResumeScore { get; set; }
        public List<string> Suggestions { get; set; } = new List<string>();
    }

    public class ResumeTemplateDto
    {
        [Required]
        [StringLength(100)]
        public string TemplateName { get; set; }

        [Required]
        [StringLength(50)]
        public string Industry { get; set; }

        [Required]
        public string TemplateContent { get; set; }

        public string PreviewImagePath { get; set; }

        public bool IsActive { get; set; } = true;
    }
}