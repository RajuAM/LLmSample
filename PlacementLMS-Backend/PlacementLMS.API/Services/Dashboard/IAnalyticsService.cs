using PlacementLMS.DTOs.Dashboard;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Dashboard
{
    public interface IAnalyticsService
    {
        Task<DashboardDto> GetDashboardDataAsync();
        Task<StudentReportDto> GetStudentReportAsync(ReportFiltersDto filters);
        Task<CourseReportDto> GetCourseReportAsync(ReportFiltersDto filters);
        Task<PlacementReportDto> GetPlacementReportAsync(ReportFiltersDto filters);
        Task<CertificateReportDto> GetCertificateReportAsync(ReportFiltersDto filters);
        Task<ProgressReportDto> GetProgressReportAsync(ReportFiltersDto filters);

        // Specific analytics methods
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
        Task<StudentAnalyticsDto> GetStudentAnalyticsAsync();
        Task<CourseAnalyticsDto> GetCourseAnalyticsAsync();
        Task<PlacementAnalyticsDto> GetPlacementAnalyticsAsync();
        Task<CertificateAnalyticsDto> GetCertificateAnalyticsAsync();

        // Trend analysis
        Task<List<MonthlyRegistrationDto>> GetStudentRegistrationTrendsAsync(int months = 12);
        Task<List<MonthlyEnrollmentDto>> GetCourseEnrollmentTrendsAsync(int months = 12);
        Task<List<MonthlyPlacementDto>> GetPlacementTrendsAsync(int months = 12);

        // Export functionality
        Task<byte[]> ExportStudentReportAsync(ReportFiltersDto filters, string format = "excel");
        Task<byte[]> ExportCourseReportAsync(ReportFiltersDto filters, string format = "excel");
        Task<byte[]> ExportPlacementReportAsync(ReportFiltersDto filters, string format = "excel");
        Task<byte[]> ExportCertificateReportAsync(ReportFiltersDto filters, string format = "excel");
    }
}