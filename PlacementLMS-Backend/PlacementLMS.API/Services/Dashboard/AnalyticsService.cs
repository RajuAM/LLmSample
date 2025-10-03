using Microsoft.EntityFrameworkCore;
using PlacementLMS.Data;
using PlacementLMS.DTOs.Dashboard;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Dashboard
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            var dashboard = new DashboardDto
            {
                Summary = await GetDashboardSummaryAsync(),
                StudentAnalytics = await GetStudentAnalyticsAsync(),
                CourseAnalytics = await GetCourseAnalyticsAsync(),
                PlacementAnalytics = await GetPlacementAnalyticsAsync(),
                CertificateAnalytics = await GetCertificateAnalyticsAsync()
            };

            return dashboard;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            var totalStudents = await _context.Students.CountAsync();
            var totalCompanies = await _context.Companies.CountAsync(c => c.IsActive);
            var totalCourses = await _context.Courses.CountAsync(c => c.IsActive);
            var totalJobOpportunities = await _context.JobOpportunities.CountAsync(j => j.IsActive);
            var totalPlacements = await _context.JobApplications.CountAsync(j => j.IsSelected);
            var totalCertificates = await _context.Certificates.CountAsync();

            // Calculate placement rate
            var totalApplications = await _context.JobApplications.CountAsync();
            var placementRate = totalApplications > 0 ? (decimal)totalPlacements / totalApplications * 100 : 0;

            // Calculate average course rating
            var courseFeedbacks = await _context.Feedbacks
                .Where(f => f.CourseId != null)
                .ToListAsync();
            var averageCourseRating = courseFeedbacks.Any() ? courseFeedbacks.Average(f => f.Rating) : 0;

            return new DashboardSummaryDto
            {
                TotalStudents = totalStudents,
                TotalCompanies = totalCompanies,
                TotalCourses = totalCourses,
                TotalJobOpportunities = totalJobOpportunities,
                TotalPlacements = totalPlacements,
                TotalCertificates = totalCertificates,
                PlacementRate = placementRate,
                AverageCourseRating = (decimal)averageCourseRating
            };
        }

        public async Task<StudentAnalyticsDto> GetStudentAnalyticsAsync()
        {
            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;

            var newRegistrationsThisMonth = await _context.Students
                .CountAsync(s => s.EnrollmentDate.Month == currentMonth && s.EnrollmentDate.Year == currentYear);

            var activeStudents = await _context.Students.CountAsync(s => s.IsProfileComplete);

            // Calculate average progress (simplified)
            var studentCourses = await _context.StudentCourses.ToListAsync();
            var totalEnrollments = studentCourses.Count;
            var completedCourses = studentCourses.Count(sc => sc.IsCompleted);
            var averageProgress = totalEnrollments > 0 ? (decimal)completedCourses / totalEnrollments * 100 : 0;

            var registrationTrends = await GetStudentRegistrationTrendsAsync(12);
            var departmentBreakdown = await GetDepartmentBreakdownAsync();
            var semesterBreakdown = await GetSemesterBreakdownAsync();

            return new StudentAnalyticsDto
            {
                NewRegistrationsThisMonth = newRegistrationsThisMonth,
                ActiveStudents = activeStudents,
                AverageProgress = averageProgress,
                RegistrationTrends = registrationTrends,
                DepartmentBreakdown = departmentBreakdown,
                SemesterBreakdown = semesterBreakdown
            };
        }

        public async Task<CourseAnalyticsDto> GetCourseAnalyticsAsync()
        {
            var totalCourses = await _context.Courses.CountAsync(c => c.IsActive);
            var totalEnrollments = await _context.StudentCourses.CountAsync();

            // Calculate completion rate
            var completedEnrollments = await _context.StudentCourses.CountAsync(sc => sc.IsCompleted);
            var averageCompletionRate = totalEnrollments > 0 ? (decimal)completedEnrollments / totalEnrollments * 100 : 0;

            var topCourses = await GetTopCoursesAsync(10);
            var categoryBreakdown = await GetCourseCategoryBreakdownAsync();
            var enrollmentTrends = await GetCourseEnrollmentTrendsAsync(12);

            return new CourseAnalyticsDto
            {
                TotalCourses = totalCourses,
                TotalEnrollments = totalEnrollments,
                AverageCompletionRate = averageCompletionRate,
                TopCourses = topCourses,
                CategoryBreakdown = categoryBreakdown,
                EnrollmentTrends = enrollmentTrends
            };
        }

        public async Task<PlacementAnalyticsDto> GetPlacementAnalyticsAsync()
        {
            var totalPlacements = await _context.JobApplications.CountAsync(j => j.IsSelected);
            var totalApplications = await _context.JobApplications.CountAsync();
            var totalJobOpportunities = await _context.JobOpportunities.CountAsync(j => j.IsActive);

            var placementRate = totalApplications > 0 ? (decimal)totalPlacements / totalApplications * 100 : 0;

            // Calculate average salary (simplified - would need salary data in JobApplication)
            var averageSalary = 50000; // Placeholder - would calculate from actual data

            var topIndustries = await GetTopIndustriesAsync(10);
            var placementTrends = await GetPlacementTrendsAsync(12);
            var topCompanies = await GetTopCompaniesAsync(10);

            return new PlacementAnalyticsDto
            {
                TotalPlacements = totalPlacements,
                TotalApplications = totalApplications,
                TotalJobOpportunities = totalJobOpportunities,
                PlacementRate = placementRate,
                AverageSalary = averageSalary,
                TopIndustries = topIndustries,
                PlacementTrends = placementTrends,
                TopCompanies = topCompanies
            };
        }

        public async Task<CertificateAnalyticsDto> GetCertificateAnalyticsAsync()
        {
            var totalCertificates = await _context.Certificates.CountAsync();

            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;
            var certificatesThisMonth = await _context.Certificates
                .CountAsync(c => c.IssueDate.Month == currentMonth && c.IssueDate.Year == currentYear);

            // Calculate average grade (simplified)
            var averageGrade = 85.5m; // Placeholder - would calculate from actual grades

            var courseBreakdown = await GetCertificateCourseBreakdownAsync();
            var recentCertificates = await GetRecentCertificatesAsync(10);

            return new CertificateAnalyticsDto
            {
                TotalCertificates = totalCertificates,
                CertificatesThisMonth = certificatesThisMonth,
                AverageGrade = averageGrade,
                CourseBreakdown = courseBreakdown,
                RecentCertificates = recentCertificates
            };
        }

        // Supporting methods for data aggregation
        private async Task<List<MonthlyRegistrationDto>> GetStudentRegistrationTrendsAsync(int months)
        {
            var trends = new List<MonthlyRegistrationDto>();
            var currentDate = DateTime.UtcNow;

            for (int i = months - 1; i >= 0; i--)
            {
                var targetDate = currentDate.AddMonths(-i);
                var count = await _context.Students
                    .CountAsync(s => s.EnrollmentDate.Year == targetDate.Year && s.EnrollmentDate.Month == targetDate.Month);

                trends.Add(new MonthlyRegistrationDto
                {
                    Month = $"{targetDate.Year}-{targetDate.Month:D2}",
                    Count = count,
                    CumulativeCount = count // Would calculate cumulative if needed
                });
            }

            return trends;
        }

        private async Task<List<DepartmentBreakdownDto>> GetDepartmentBreakdownAsync()
        {
            var departmentGroups = await _context.Students
                .GroupBy(s => s.Department)
                .Select(g => new { Department = g.Key, Count = g.Count() })
                .ToListAsync();

            var total = departmentGroups.Sum(g => g.Count);
            return departmentGroups.Select(g => new DepartmentBreakdownDto
            {
                Department = g.Department,
                StudentCount = g.Count,
                Percentage = total > 0 ? (decimal)g.Count / total * 100 : 0
            }).ToList();
        }

        private async Task<List<SemesterBreakdownDto>> GetSemesterBreakdownAsync()
        {
            var semesterGroups = await _context.Students
                .GroupBy(s => s.CurrentSemester)
                .Select(g => new { Semester = g.Key, Count = g.Count() })
                .ToListAsync();

            var total = semesterGroups.Sum(g => g.Count);
            return semesterGroups.Select(g => new SemesterBreakdownDto
            {
                Semester = g.Semester,
                StudentCount = g.Count,
                Percentage = total > 0 ? (decimal)g.Count / total * 100 : 0
            }).ToList();
        }

        private async Task<List<CourseSubscriptionDto>> GetTopCoursesAsync(int count)
        {
            return await _context.Courses
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.StudentCourses.Count)
                .Take(count)
                .Select(c => new CourseSubscriptionDto
                {
                    CourseId = c.Id,
                    CourseTitle = c.Title,
                    EnrollmentCount = c.StudentCourses.Count,
                    AverageRating = c.CourseFeedback.Any() ? (decimal)c.CourseFeedback.Average(f => f.Rating) : 0,
                    CompletionRate = c.StudentCourses.Any() ?
                        (int)(c.StudentCourses.Count(sc => sc.IsCompleted) * 100.0 / c.StudentCourses.Count) : 0
                })
                .ToListAsync();
        }

        private async Task<List<CategoryBreakdownDto>> GetCourseCategoryBreakdownAsync()
        {
            var categoryGroups = await _context.Courses
                .Where(c => c.IsActive)
                .GroupBy(c => c.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToListAsync();

            var total = categoryGroups.Sum(g => g.Count);
            return categoryGroups.Select(g => new CategoryBreakdownDto
            {
                Category = g.Category,
                Count = g.Count,
                Percentage = total > 0 ? (decimal)g.Count / total * 100 : 0
            }).ToList();
        }

        private async Task<List<MonthlyEnrollmentDto>> GetCourseEnrollmentTrendsAsync(int months)
        {
            var trends = new List<MonthlyEnrollmentDto>();
            var currentDate = DateTime.UtcNow;

            for (int i = months - 1; i >= 0; i--)
            {
                var targetDate = currentDate.AddMonths(-i);
                var count = await _context.StudentCourses
                    .CountAsync(sc => sc.EnrollmentDate.Year == targetDate.Year && sc.EnrollmentDate.Month == targetDate.Month);

                trends.Add(new MonthlyEnrollmentDto
                {
                    Month = $"{targetDate.Year}-{targetDate.Month:D2}",
                    EnrollmentCount = count
                });
            }

            return trends;
        }

        private async Task<List<IndustryPlacementDto>> GetTopIndustriesAsync(int count)
        {
            // This would require industry data from companies and job applications
            // Simplified implementation
            return new List<IndustryPlacementDto>
            {
                new IndustryPlacementDto { Industry = "IT", PlacementCount = 150, AverageSalary = 60000 },
                new IndustryPlacementDto { Industry = "Finance", PlacementCount = 89, AverageSalary = 55000 }
            };
        }

        private async Task<List<MonthlyPlacementDto>> GetPlacementTrendsAsync(int months)
        {
            var trends = new List<MonthlyPlacementDto>();
            var currentDate = DateTime.UtcNow;

            for (int i = months - 1; i >= 0; i--)
            {
                var targetDate = currentDate.AddMonths(-i);
                var count = await _context.JobApplications
                    .CountAsync(ja => ja.SelectionDate.HasValue &&
                        ja.SelectionDate.Value.Year == targetDate.Year &&
                        ja.SelectionDate.Value.Month == targetDate.Month);

                trends.Add(new MonthlyPlacementDto
                {
                    Month = $"{targetDate.Year}-{targetDate.Month:D2}",
                    PlacementCount = count,
                    SuccessRate = 75.5m // Would calculate actual rate
                });
            }

            return trends;
        }

        private async Task<List<CompanyPlacementDto>> GetTopCompaniesAsync(int count)
        {
            return await _context.JobApplications
                .Where(ja => ja.IsSelected)
                .Join(_context.JobOpportunities,
                    ja => ja.JobOpportunityId,
                    jo => jo.Id,
                    (ja, jo) => new { ja, jo })
                .Join(_context.Companies,
                    j => j.jo.CompanyId,
                    c => c.Id,
                    (j, c) => new { j.ja, j.jo, c })
                .GroupBy(x => new { x.c.Id, x.c.Name })
                .Select(g => new CompanyPlacementDto
                {
                    CompanyName = g.Key.Name,
                    PlacementCount = g.Count(),
                    AverageSalary = 55000 // Would calculate from actual salary data
                })
                .OrderByDescending(c => c.PlacementCount)
                .Take(count)
                .ToListAsync();
        }

        private async Task<List<CertificateBreakdownDto>> GetCertificateCourseBreakdownAsync()
        {
            return await _context.Certificates
                .GroupBy(c => c.CourseName)
                .Select(g => new CertificateBreakdownDto
                {
                    CourseName = g.Key,
                    CertificateCount = g.Count(),
                    AverageGrade = 85.5m // Would calculate actual average
                })
                .ToListAsync();
        }

        private async Task<List<RecentCertificateDto>> GetRecentCertificatesAsync(int count)
        {
            return await _context.Certificates
                .OrderByDescending(c => c.IssueDate)
                .Take(count)
                .Select(c => new RecentCertificateDto
                {
                    Id = c.Id,
                    StudentName = c.Student.User.FirstName + " " + c.Student.User.LastName,
                    CourseName = c.CourseName,
                    Grade = "A", // Would get from actual grade data
                    IssueDate = c.IssueDate
                })
                .ToListAsync();
        }

        // Placeholder implementations for report methods
        public async Task<StudentReportDto> GetStudentReportAsync(ReportFiltersDto filters)
        {
            // Implementation would filter students based on criteria
            return new StudentReportDto();
        }

        public async Task<CourseReportDto> GetCourseReportAsync(ReportFiltersDto filters)
        {
            // Implementation would filter courses based on criteria
            return new CourseReportDto();
        }

        public async Task<PlacementReportDto> GetPlacementReportAsync(ReportFiltersDto filters)
        {
            // Implementation would filter placements based on criteria
            return new PlacementReportDto();
        }

        public async Task<CertificateReportDto> GetCertificateReportAsync(ReportFiltersDto filters)
        {
            // Implementation would filter certificates based on criteria
            return new CertificateReportDto();
        }

        public async Task<ProgressReportDto> GetProgressReportAsync(ReportFiltersDto filters)
        {
            // Implementation would get student progress data
            return new ProgressReportDto();
        }

        public async Task<byte[]> ExportStudentReportAsync(ReportFiltersDto filters, string format = "excel")
        {
            // Implementation would generate Excel/PDF export
            return Array.Empty<byte>();
        }

        public async Task<byte[]> ExportCourseReportAsync(ReportFiltersDto filters, string format = "excel")
        {
            // Implementation would generate Excel/PDF export
            return Array.Empty<byte>();
        }

        public async Task<byte[]> ExportPlacementReportAsync(ReportFiltersDto filters, string format = "excel")
        {
            // Implementation would generate Excel/PDF export
            return Array.Empty<byte>();
        }

        public async Task<byte[]> ExportCertificateReportAsync(ReportFiltersDto filters, string format = "excel")
        {
            // Implementation would generate Excel/PDF export
            return Array.Empty<byte>();
        }
    }
}