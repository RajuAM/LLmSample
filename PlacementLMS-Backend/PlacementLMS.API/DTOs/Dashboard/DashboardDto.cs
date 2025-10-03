using PlacementLMS.Models;

namespace PlacementLMS.DTOs.Dashboard
{
    public class DashboardDto
    {
        public DashboardSummaryDto Summary { get; set; }
        public StudentAnalyticsDto StudentAnalytics { get; set; }
        public CourseAnalyticsDto CourseAnalytics { get; set; }
        public PlacementAnalyticsDto PlacementAnalytics { get; set; }
        public CertificateAnalyticsDto CertificateAnalytics { get; set; }
    }

    public class DashboardSummaryDto
    {
        public int TotalStudents { get; set; }
        public int TotalCompanies { get; set; }
        public int TotalCourses { get; set; }
        public int TotalJobOpportunities { get; set; }
        public int TotalPlacements { get; set; }
        public int TotalCertificates { get; set; }
        public decimal PlacementRate { get; set; }
        public decimal AverageCourseRating { get; set; }
    }

    public class StudentAnalyticsDto
    {
        public int NewRegistrationsThisMonth { get; set; }
        public int ActiveStudents { get; set; }
        public decimal AverageProgress { get; set; }
        public List<MonthlyRegistrationDto> RegistrationTrends { get; set; }
        public List<DepartmentBreakdownDto> DepartmentBreakdown { get; set; }
        public List<SemesterBreakdownDto> SemesterBreakdown { get; set; }
    }

    public class CourseAnalyticsDto
    {
        public int TotalCourses { get; set; }
        public int TotalEnrollments { get; set; }
        public decimal AverageCompletionRate { get; set; }
        public List<CourseSubscriptionDto> TopCourses { get; set; }
        public List<CategoryBreakdownDto> CategoryBreakdown { get; set; }
        public List<MonthlyEnrollmentDto> EnrollmentTrends { get; set; }
    }

    public class PlacementAnalyticsDto
    {
        public int TotalPlacements { get; set; }
        public int TotalApplications { get; set; }
        public int TotalJobOpportunities { get; set; }
        public decimal PlacementRate { get; set; }
        public decimal AverageSalary { get; set; }
        public List<IndustryPlacementDto> TopIndustries { get; set; }
        public List<MonthlyPlacementDto> PlacementTrends { get; set; }
        public List<CompanyPlacementDto> TopCompanies { get; set; }
    }

    public class CertificateAnalyticsDto
    {
        public int TotalCertificates { get; set; }
        public int CertificatesThisMonth { get; set; }
        public decimal AverageGrade { get; set; }
        public List<CertificateBreakdownDto> CourseBreakdown { get; set; }
        public List<RecentCertificateDto> RecentCertificates { get; set; }
    }

    // Supporting DTOs
    public class DepartmentBreakdownDto
    {
        public string Department { get; set; }
        public int StudentCount { get; set; }
        public decimal Percentage { get; set; }
    }

    public class SemesterBreakdownDto
    {
        public int Semester { get; set; }
        public int StudentCount { get; set; }
        public decimal Percentage { get; set; }
    }

    public class MonthlyEnrollmentDto
    {
        public string Month { get; set; }
        public int EnrollmentCount { get; set; }
    }

    public class CompanyPlacementDto
    {
        public string CompanyName { get; set; }
        public int PlacementCount { get; set; }
        public decimal AverageSalary { get; set; }
    }

    public class RecentCertificateDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public string Grade { get; set; }
        public DateTime IssueDate { get; set; }
    }
}