using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.Models
{
    public class DashboardAnalytics
    {
        public int TotalStudents { get; set; }
        public int TotalCompanies { get; set; }
        public int TotalCourses { get; set; }
        public int TotalInstitutions { get; set; }
        public int TotalJobOpportunities { get; set; }
        public int TotalJobApplications { get; set; }
        public int TotalPlacements { get; set; }
        public int TotalCertificates { get; set; }

        // Registration trends
        public List<MonthlyRegistrationDto> StudentRegistrations { get; set; }
        public List<MonthlyRegistrationDto> CompanyRegistrations { get; set; }

        // Course analytics
        public List<CourseSubscriptionDto> CourseSubscriptions { get; set; }
        public List<CategoryBreakdownDto> CourseCategoryBreakdown { get; set; }

        // Student progress
        public List<StudentProgressDto> StudentProgressData { get; set; }
        public List<TestScoreDto> TestScoreAnalytics { get; set; }

        // Placement analytics
        public PlacementSuccessDto PlacementSuccess { get; set; }
        public List<IndustryPlacementDto> IndustryPlacements { get; set; }
        public List<MonthlyPlacementDto> MonthlyPlacements { get; set; }

        // Certificate analytics
        public List<CertificateDto> RecentCertificates { get; set; }
        public List<CertificateBreakdownDto> CertificateBreakdown { get; set; }
    }

    public class MonthlyRegistrationDto
    {
        public string Month { get; set; }
        public int Count { get; set; }
        public int CumulativeCount { get; set; }
    }

    public class CourseSubscriptionDto
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int EnrollmentCount { get; set; }
        public decimal AverageRating { get; set; }
        public int CompletionRate { get; set; }
    }

    public class CategoryBreakdownDto
    {
        public string Category { get; set; }
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }

    public class StudentProgressDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CompletedCourses { get; set; }
        public int TotalCourses { get; set; }
        public decimal AverageScore { get; set; }
        public int CertificatesEarned { get; set; }
        public bool HasJobPlacement { get; set; }
    }

    public class TestScoreDto
    {
        public string TestType { get; set; }
        public int TotalTests { get; set; }
        public decimal AverageScore { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
        public decimal PassRate { get; set; }
    }

    public class PlacementSuccessDto
    {
        public int TotalPlacements { get; set; }
        public decimal PlacementRate { get; set; }
        public decimal AverageSalary { get; set; }
        public int TotalOffers { get; set; }
        public decimal OfferAcceptanceRate { get; set; }
    }

    public class IndustryPlacementDto
    {
        public string Industry { get; set; }
        public int PlacementCount { get; set; }
        public decimal AverageSalary { get; set; }
        public List<string> TopCompanies { get; set; }
    }

    public class MonthlyPlacementDto
    {
        public string Month { get; set; }
        public int PlacementCount { get; set; }
        public decimal SuccessRate { get; set; }
    }

    public class CertificateDto
    {
        public int CertificateId { get; set; }
        public string CertificateTitle { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public DateTime IssueDate { get; set; }
        public string Grade { get; set; }
    }

    public class CertificateBreakdownDto
    {
        public string CourseName { get; set; }
        public int CertificateCount { get; set; }
        public decimal AverageGrade { get; set; }
    }
}