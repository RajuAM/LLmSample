using System.ComponentModel.DataAnnotations;
using PlacementLMS.DTOs.Placement;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Industry
{
    public interface IIndustryService
    {
        // Company Management
        Task<Models.Industry> RegisterCompanyAsync(CompanyRegistrationDto companyDto);
        Task<IndustryResponseDto> GetCompanyProfileAsync(int industryId);
        Task<IndustryResponseDto> UpdateCompanyProfileAsync(int industryId, CompanyUpdateDto companyDto);

        // Job Management
        Task<JobOpportunity> CreateJobOpportunityAsync(JobOpportunityDto jobDto);
        Task<IEnumerable<JobOpportunityResponseDto>> GetAllJobOpportunitiesAsync();
        Task<JobOpportunityResponseDto> GetJobOpportunityByIdAsync(int jobId);
        Task UpdateJobOpportunityAsync(int jobId, JobOpportunityDto jobDto);
        Task DeleteJobOpportunityAsync(int jobId);
        Task<IEnumerable<JobOpportunityResponseDto>> GetJobsByIndustryAsync(int industryId);
        Task<IEnumerable<JobOpportunityResponseDto>> GetJobsBySkillsAsync(string skills);

        // Application Management
        Task<IEnumerable<JobApplicationResponseDto>> GetJobApplicationsAsync(int jobId);
        Task<JobApplicationResponseDto> GetApplicationByIdAsync(int applicationId);
        Task UpdateApplicationStatusAsync(int applicationId, string status, string feedback);
        Task ScheduleInterviewAsync(InterviewScheduleDto scheduleDto);
        Task ProvideInterviewFeedbackAsync(InterviewFeedbackDto feedbackDto);

        // Analytics
        Task<IndustryAnalyticsDto> GetIndustryAnalyticsAsync(int industryId);
        Task<IEnumerable<JobApplicationResponseDto>> GetTopCandidatesAsync(int industryId);
    }

    public class CompanyRegistrationDto
    {
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

        [Required]
        [StringLength(200)]
        public string IndustryType { get; set; }

        public int EmployeeCount { get; set; }

        public decimal RegistrationFee { get; set; }
    }

    public class CompanyUpdateDto
    {
        [StringLength(200)]
        public string CompanyName { get; set; }

        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(20)]
        public string Pincode { get; set; }

        [StringLength(15)]
        public string ContactNumber { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }

        public string Website { get; set; }

        [StringLength(200)]
        public string IndustryType { get; set; }

        public int EmployeeCount { get; set; }
    }

    public class IndustryResponseDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string Website { get; set; }
        public string IndustryType { get; set; }
        public int EmployeeCount { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalJobOpportunities { get; set; }
        public int TotalApplications { get; set; }
        public int TotalSelections { get; set; }
    }

    public class IndustryAnalyticsDto
    {
        public int IndustryId { get; set; }
        public string CompanyName { get; set; }
        public int TotalJobOpportunities { get; set; }
        public int TotalApplications { get; set; }
        public int TotalInterviews { get; set; }
        public int TotalSelections { get; set; }
        public double SelectionRate { get; set; }
        public List<MonthlyStatsDto> MonthlyStats { get; set; } = new List<MonthlyStatsDto>();
        public List<SkillDemandDto> TopSkills { get; set; } = new List<SkillDemandDto>();
    }

    public class MonthlyStatsDto
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Applications { get; set; }
        public int Selections { get; set; }
    }

    public class SkillDemandDto
    {
        public string Skill { get; set; }
        public int Count { get; set; }
    }
}