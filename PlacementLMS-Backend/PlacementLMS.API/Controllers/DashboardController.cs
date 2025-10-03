using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementLMS.DTOs.Dashboard;
using PlacementLMS.Services.Dashboard;

namespace PlacementLMS.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public DashboardController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        #region Dashboard Overview
        [HttpGet("overview")]
        public async Task<IActionResult> GetDashboardOverview()
        {
            try
            {
                var dashboardData = await _analyticsService.GetDashboardDataAsync();
                return Ok(dashboardData);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            try
            {
                var summary = await _analyticsService.GetDashboardSummaryAsync();
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Student Analytics
        [HttpGet("students")]
        public async Task<IActionResult> GetStudentAnalytics()
        {
            try
            {
                var studentAnalytics = await _analyticsService.GetStudentAnalyticsAsync();
                return Ok(studentAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("students/trends")]
        public async Task<IActionResult> GetStudentRegistrationTrends([FromQuery] int months = 12)
        {
            try
            {
                var trends = await _analyticsService.GetStudentRegistrationTrendsAsync(months);
                return Ok(trends);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("students/report")]
        public async Task<IActionResult> GetStudentReport([FromQuery] ReportFiltersDto filters)
        {
            try
            {
                var report = await _analyticsService.GetStudentReportAsync(filters);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Course Analytics
        [HttpGet("courses")]
        public async Task<IActionResult> GetCourseAnalytics()
        {
            try
            {
                var courseAnalytics = await _analyticsService.GetCourseAnalyticsAsync();
                return Ok(courseAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("courses/trends")]
        public async Task<IActionResult> GetCourseEnrollmentTrends([FromQuery] int months = 12)
        {
            try
            {
                var trends = await _analyticsService.GetCourseEnrollmentTrendsAsync(months);
                return Ok(trends);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("courses/report")]
        public async Task<IActionResult> GetCourseReport([FromQuery] ReportFiltersDto filters)
        {
            try
            {
                var report = await _analyticsService.GetCourseReportAsync(filters);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Placement Analytics
        [HttpGet("placements")]
        public async Task<IActionResult> GetPlacementAnalytics()
        {
            try
            {
                var placementAnalytics = await _analyticsService.GetPlacementAnalyticsAsync();
                return Ok(placementAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("placements/trends")]
        public async Task<IActionResult> GetPlacementTrends([FromQuery] int months = 12)
        {
            try
            {
                var trends = await _analyticsService.GetPlacementTrendsAsync(months);
                return Ok(trends);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("placements/report")]
        public async Task<IActionResult> GetPlacementReport([FromQuery] ReportFiltersDto filters)
        {
            try
            {
                var report = await _analyticsService.GetPlacementReportAsync(filters);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Certificate Analytics
        [HttpGet("certificates")]
        public async Task<IActionResult> GetCertificateAnalytics()
        {
            try
            {
                var certificateAnalytics = await _analyticsService.GetCertificateAnalyticsAsync();
                return Ok(certificateAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("certificates/report")]
        public async Task<IActionResult> GetCertificateReport([FromQuery] ReportFiltersDto filters)
        {
            try
            {
                var report = await _analyticsService.GetCertificateReportAsync(filters);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Progress Analytics
        [HttpGet("progress")]
        public async Task<IActionResult> GetProgressAnalytics()
        {
            try
            {
                var progressAnalytics = await _analyticsService.GetProgressReportAsync(new ReportFiltersDto());
                return Ok(progressAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("progress/report")]
        public async Task<IActionResult> GetProgressReport([FromQuery] ReportFiltersDto filters)
        {
            try
            {
                var report = await _analyticsService.GetProgressReportAsync(filters);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Export Functionality
        [HttpGet("export/students")]
        public async Task<IActionResult> ExportStudentReport([FromQuery] ReportFiltersDto filters, [FromQuery] string format = "excel")
        {
            try
            {
                var fileBytes = await _analyticsService.ExportStudentReportAsync(filters, format);

                var contentType = format.ToLower() == "excel"
                    ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    : "application/pdf";

                var fileName = $"StudentReport_{DateTime.UtcNow:yyyyMMdd}.{format}";

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("export/courses")]
        public async Task<IActionResult> ExportCourseReport([FromQuery] ReportFiltersDto filters, [FromQuery] string format = "excel")
        {
            try
            {
                var fileBytes = await _analyticsService.ExportCourseReportAsync(filters, format);

                var contentType = format.ToLower() == "excel"
                    ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    : "application/pdf";

                var fileName = $"CourseReport_{DateTime.UtcNow:yyyyMMdd}.{format}";

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("export/placements")]
        public async Task<IActionResult> ExportPlacementReport([FromQuery] ReportFiltersDto filters, [FromQuery] string format = "excel")
        {
            try
            {
                var fileBytes = await _analyticsService.ExportPlacementReportAsync(filters, format);

                var contentType = format.ToLower() == "excel"
                    ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    : "application/pdf";

                var fileName = $"PlacementReport_{DateTime.UtcNow:yyyyMMdd}.{format}";

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("export/certificates")]
        public async Task<IActionResult> ExportCertificateReport([FromQuery] ReportFiltersDto filters, [FromQuery] string format = "excel")
        {
            try
            {
                var fileBytes = await _analyticsService.ExportCertificateReportAsync(filters, format);

                var contentType = format.ToLower() == "excel"
                    ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    : "application/pdf";

                var fileName = $"CertificateReport_{DateTime.UtcNow:yyyyMMdd}.{format}";

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Real-time Data
        [HttpGet("realtime/summary")]
        public async Task<IActionResult> GetRealTimeSummary()
        {
            try
            {
                // Get latest data for real-time dashboard updates
                var summary = await _analyticsService.GetDashboardSummaryAsync();
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("realtime/activity")]
        public async Task<IActionResult> GetRealTimeActivity()
        {
            try
            {
                // Get recent activities for real-time updates
                var recentActivity = new
                {
                    RecentRegistrations = 5, // Would get actual count
                    RecentPlacements = 3,    // Would get actual count
                    RecentCertificates = 7,  // Would get actual count
                    RecentCourseEnrollments = 12 // Would get actual count
                };

                return Ok(recentActivity);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion
    }
}