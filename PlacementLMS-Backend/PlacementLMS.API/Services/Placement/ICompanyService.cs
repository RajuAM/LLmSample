using PlacementLMS.DTOs.Placement;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Placement
{
    public interface ICompanyService
    {
        Task<Company> RegisterCompanyAsync(CompanyRegistrationDto registrationDto);
        Task<Company> GetCompanyByIdAsync(int companyId);
        Task UpdateCompanyAsync(int companyId, CompanyUpdateDto updateDto);
        Task<JobOpportunity> CreateJobOpportunityAsync(int companyId, JobOpportunityDto jobDto);
        Task<IEnumerable<JobOpportunityResponseDto>> GetCompanyJobOpportunitiesAsync(int companyId);
        Task<JobOpportunity> GetJobOpportunityByIdAsync(int jobId);
        Task UpdateJobOpportunityAsync(int jobId, JobOpportunityDto jobDto);
        Task DeleteJobOpportunityAsync(int jobId);
        Task<IEnumerable<JobApplicationResponseDto>> GetJobApplicationsAsync(int jobId);
        Task UpdateApplicationStatusAsync(int applicationId, string status, string feedback);
        Task ScheduleInterviewAsync(int applicationId, DateTime interviewDate, string notes);
        Task<IEnumerable<JobApplicationResponseDto>> GetPendingApplicationsAsync(int companyId);
        Task<IEnumerable<JobApplicationResponseDto>> GetShortlistedApplicationsAsync(int companyId);
        Task<PlacementStatsDto> GetCompanyAnalyticsAsync(int companyId);
        Task<JobOpportunityResponseDto> GetJobAnalyticsAsync(int jobId);
    }
}