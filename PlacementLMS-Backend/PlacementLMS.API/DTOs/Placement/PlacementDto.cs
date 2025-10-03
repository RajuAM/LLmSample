using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Placement
{
    public class JobOpportunityDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int IndustryId { get; set; }

        [Required]
        [StringLength(100)]
        public string JobType { get; set; }

        [Required]
        [StringLength(100)]
        public string ExperienceLevel { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SalaryMin { get; set; }

        public decimal SalaryMax { get; set; }

        [Required]
        public string SkillsRequired { get; set; }

        public string Responsibilities { get; set; }

        public string Benefits { get; set; }

        [Required]
        [Range(1, 1000)]
        public int NumberOfPositions { get; set; }

        public DateTime ApplicationDeadline { get; set; }
    }

    public class JobOpportunityResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string JobType { get; set; }
        public string ExperienceLevel { get; set; }
        public string Location { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string SkillsRequired { get; set; }
        public string Responsibilities { get; set; }
        public string Benefits { get; set; }
        public int NumberOfPositions { get; set; }
        public int NumberOfApplications { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<JobApplicationDto> Applications { get; set; } = new List<JobApplicationDto>();
    }

    public class JobApplicationDto
    {
        [Required]
        public int JobOpportunityId { get; set; }

        public string CoverLetter { get; set; }

        public string ResumeId { get; set; }
    }

    public class JobApplicationResponseDto
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string StudentName { get; set; }
        public DateTime AppliedAt { get; set; }
        public string Status { get; set; }
        public string CoverLetter { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string InterviewFeedback { get; set; }
        public bool IsSelected { get; set; }
        public DateTime? SelectionDate { get; set; }
    }

    public class InterviewScheduleDto
    {
        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public DateTime InterviewDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        public string InterviewerName { get; set; }

        public string Notes { get; set; }
    }

    public class InterviewFeedbackDto
    {
        [Required]
        public int ApplicationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public string Feedback { get; set; }

        public bool IsSelected { get; set; }

        public DateTime? SelectionDate { get; set; }
    }

    public class PlacementStatsDto
    {
        public int TotalJobOpportunities { get; set; }
        public int TotalApplications { get; set; }
        public int TotalInterviews { get; set; }
        public int TotalSelections { get; set; }
        public double PlacementRate { get; set; }
        public List<CompanyStatsDto> TopCompanies { get; set; } = new List<CompanyStatsDto>();
        public List<SkillDemandDto> TopSkills { get; set; } = new List<SkillDemandDto>();
    }

    public class CompanyStatsDto
    {
        public string CompanyName { get; set; }
        public int JobOpportunities { get; set; }
        public int TotalApplications { get; set; }
        public int Selections { get; set; }
    }

    public class SkillDemandDto
    {
        public string Skill { get; set; }
        public int DemandCount { get; set; }
    }

    public class CompanyRegistrationDto
    {
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
        public string CompanySize { get; set; }

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

        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactEmail { get; set; }

        [StringLength(100)]
        public string HRContactName { get; set; }

        [StringLength(20)]
        public string HRContactNumber { get; set; }

        [StringLength(100)]
        public string HRContactEmail { get; set; }
    }

    public class CompanyUpdateDto
    {
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Website { get; set; }

        [StringLength(100)]
        public string CompanySize { get; set; }

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
    }

    public class ApplicationStatusDto
    {
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public string Feedback { get; set; }
    }
}