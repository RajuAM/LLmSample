namespace PlacementLMS.DTOs.Dashboard
{
    public class ReportDto
    {
        public string ReportType { get; set; }
        public DateTime GeneratedAt { get; set; }
        public ReportFiltersDto Filters { get; set; }
        public object Data { get; set; }
    }

    public class ReportFiltersDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Department { get; set; }
        public string CourseCategory { get; set; }
        public string Industry { get; set; }
        public int? Semester { get; set; }
    }

    public class StudentReportDto
    {
        public List<StudentDetailDto> Students { get; set; }
        public int TotalCount { get; set; }
        public Dictionary<string, int> StatusBreakdown { get; set; }
        public Dictionary<string, int> DepartmentBreakdown { get; set; }
    }

    public class StudentDetailDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Department { get; set; }
        public int Semester { get; set; }
        public decimal CGPA { get; set; }
        public int CompletedCourses { get; set; }
        public int TotalCourses { get; set; }
        public int CertificatesEarned { get; set; }
        public bool HasPlacement { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }
    }

    public class CourseReportDto
    {
        public List<CourseDetailDto> Courses { get; set; }
        public int TotalCourses { get; set; }
        public Dictionary<string, int> CategoryBreakdown { get; set; }
        public Dictionary<string, int> InstitutionBreakdown { get; set; }
    }

    public class CourseDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string InstitutionName { get; set; }
        public int EnrollmentCount { get; set; }
        public int CompletionCount { get; set; }
        public decimal CompletionRate { get; set; }
        public decimal AverageRating { get; set; }
        public decimal Price { get; set; }
        public int DurationHours { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PlacementReportDto
    {
        public List<PlacementDetailDto> Placements { get; set; }
        public int TotalPlacements { get; set; }
        public Dictionary<string, int> IndustryBreakdown { get; set; }
        public Dictionary<string, int> CompanyBreakdown { get; set; }
        public SalaryAnalyticsDto SalaryAnalytics { get; set; }
    }

    public class PlacementDetailDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string Industry { get; set; }
        public decimal Salary { get; set; }
        public DateTime PlacementDate { get; set; }
        public string JobType { get; set; }
        public string Location { get; set; }
    }

    public class SalaryAnalyticsDto
    {
        public decimal AverageSalary { get; set; }
        public decimal MedianSalary { get; set; }
        public decimal HighestSalary { get; set; }
        public decimal LowestSalary { get; set; }
        public Dictionary<string, decimal> SalaryByIndustry { get; set; }
        public Dictionary<string, decimal> SalaryByExperience { get; set; }
    }

    public class CertificateReportDto
    {
        public List<CertificateDetailDto> Certificates { get; set; }
        public int TotalCertificates { get; set; }
        public Dictionary<string, int> CourseBreakdown { get; set; }
        public Dictionary<string, int> GradeBreakdown { get; set; }
    }

    public class CertificateDetailDto
    {
        public int Id { get; set; }
        public string CertificateTitle { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public string InstitutionName { get; set; }
        public string Grade { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class ProgressReportDto
    {
        public List<StudentProgressDetailDto> StudentProgress { get; set; }
        public TestScoreReportDto TestScores { get; set; }
        public AssignmentReportDto Assignments { get; set; }
    }

    public class StudentProgressDetailDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int TotalCoursesEnrolled { get; set; }
        public int CompletedCourses { get; set; }
        public int InProgressCourses { get; set; }
        public decimal AverageScore { get; set; }
        public int TotalAssignments { get; set; }
        public int CompletedAssignments { get; set; }
        public int TotalTests { get; set; }
        public int PassedTests { get; set; }
        public DateTime LastActivity { get; set; }
    }

    public class TestScoreReportDto
    {
        public List<TestDetailDto> Tests { get; set; }
        public decimal OverallAverageScore { get; set; }
        public int TotalTests { get; set; }
        public int PassedTests { get; set; }
        public int FailedTests { get; set; }
        public decimal PassRate { get; set; }
    }

    public class TestDetailDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string CourseName { get; set; }
        public string StudentName { get; set; }
        public decimal Score { get; set; }
        public decimal MaxScore { get; set; }
        public string Grade { get; set; }
        public DateTime AttemptDate { get; set; }
        public int TimeTakenMinutes { get; set; }
    }

    public class AssignmentReportDto
    {
        public List<AssignmentDetailDto> Assignments { get; set; }
        public int TotalAssignments { get; set; }
        public int SubmittedAssignments { get; set; }
        public int GradedAssignments { get; set; }
        public decimal AverageGrade { get; set; }
    }

    public class AssignmentDetailDto
    {
        public int Id { get; set; }
        public string AssignmentTitle { get; set; }
        public string CourseName { get; set; }
        public string StudentName { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string Status { get; set; }
        public decimal? Score { get; set; }
        public string Grade { get; set; }
    }
}