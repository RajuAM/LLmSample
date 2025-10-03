using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementLMS.DTOs.Course;
using PlacementLMS.DTOs.MockTest;
using PlacementLMS.DTOs.Placement;
using PlacementLMS.DTOs.Resume;
using PlacementLMS.DTOs.Student;
using PlacementLMS.Services.Student;

namespace PlacementLMS.Controllers
{
    [ApiController]
    [Route("api/student")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        #region Profile Management
        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile([FromBody] StudentRegistrationDto profileDto)
        {
            try
            {
                // Get user ID from JWT token
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "1");
                var student = await _studentService.CompleteStudentProfileAsync(userId, profileDto);
                return Ok(new { Message = "Profile completed successfully", StudentId = student.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var profile = await _studentService.GetStudentProfileAsync(studentId);
                if (profile == null)
                    return NotFound(new { Message = "Profile not found" });

                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] StudentProfileDto profileDto)
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var profile = await _studentService.UpdateStudentProfileAsync(studentId, profileDto);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Course Management
        [HttpGet("courses/available")]
        public async Task<IActionResult> GetAvailableCourses()
        {
            var courses = await _studentService.GetAvailableCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("courses/{courseId}")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            var course = await _studentService.GetCourseByIdAsync(courseId);
            if (course == null)
                return NotFound(new { Message = "Course not found" });

            return Ok(course);
        }

        [HttpPost("courses/enroll")]
        public async Task<IActionResult> EnrollInCourse([FromBody] CourseEnrollmentDto enrollmentDto)
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var enrollment = await _studentService.EnrollInCourseAsync(studentId, enrollmentDto);
                return Ok(new { Message = "Enrolled successfully", EnrollmentId = enrollment.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("courses/enrolled")]
        public async Task<IActionResult> GetEnrolledCourses()
        {
            var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
            var courses = await _studentService.GetEnrolledCoursesAsync(studentId);
            return Ok(courses);
        }

        [HttpGet("courses/{courseId}/progress")]
        public async Task<IActionResult> GetCourseProgress(int courseId)
        {
            var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
            var progress = await _studentService.GetCourseProgressAsync(studentId, courseId);
            if (progress == null)
                return NotFound(new { Message = "Course progress not found" });

            return Ok(progress);
        }
        #endregion

        #region Assignment Management
        [HttpGet("courses/{courseId}/assignments")]
        public async Task<IActionResult> GetCourseAssignments(int courseId)
        {
            var assignments = await _studentService.GetCourseAssignmentsAsync(courseId);
            return Ok(assignments);
        }

        [HttpPost("assignments/submit")]
        public async Task<IActionResult> SubmitAssignment([FromBody] StudentAssignmentDto submissionDto)
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var result = await _studentService.SubmitAssignmentAsync(studentId, submissionDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("assignments")]
        public async Task<IActionResult> GetStudentAssignments()
        {
            var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
            var assignments = await _studentService.GetStudentAssignmentsAsync(studentId);
            return Ok(assignments);
        }
        #endregion

        #region Mock Test Management
        [HttpGet("mock-tests/available")]
        public async Task<IActionResult> GetAvailableMockTests()
        {
            var tests = await _studentService.GetAvailableMockTestsAsync();
            return Ok(tests);
        }

        [HttpGet("mock-tests/{testId}")]
        public async Task<IActionResult> GetMockTestById(int testId)
        {
            var test = await _studentService.GetMockTestByIdAsync(testId);
            if (test == null)
                return NotFound(new { Message = "Mock test not found" });

            return Ok(test);
        }

        [HttpPost("mock-tests/{testId}/start")]
        public async Task<IActionResult> StartMockTest(int testId)
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var result = await _studentService.StartMockTestAsync(studentId, testId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("mock-tests/{testId}/submit")]
        public async Task<IActionResult> SubmitMockTest(int testId, [FromBody] StudentMockTestDto submissionDto)
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var result = await _studentService.SubmitMockTestAsync(studentId, testId, submissionDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("mock-tests/results")]
        public async Task<IActionResult> GetTestResults()
        {
            var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
            var results = await _studentService.GetStudentTestResultsAsync(studentId);
            return Ok(results);
        }
        #endregion

        #region Resume Management
        [HttpPost("resume")]
        public async Task<IActionResult> CreateOrUpdateResume([FromBody] ResumeDto resumeDto)
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var result = await _studentService.CreateOrUpdateResumeAsync(studentId, resumeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("resume")]
        public async Task<IActionResult> GetResume()
        {
            var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
            var resume = await _studentService.GetStudentResumeAsync(studentId);
            return Ok(resume);
        }

        [HttpGet("resume/templates")]
        public async Task<IActionResult> GetResumeTemplates()
        {
            var templates = await _studentService.GetResumeTemplatesAsync();
            return Ok(templates);
        }

        [HttpPost("resume/generate/{templateId}")]
        public async Task<IActionResult> GenerateResumeFromTemplate(int templateId)
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var result = await _studentService.GenerateResumeFromTemplateAsync(studentId, templateId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Job Application Management
        [HttpGet("jobs/available")]
        public async Task<IActionResult> GetAvailableJobOpportunities()
        {
            var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
            var jobs = await _studentService.GetAvailableJobOpportunitiesAsync(studentId);
            return Ok(jobs);
        }

        [HttpGet("jobs/{jobId}")]
        public async Task<IActionResult> GetJobOpportunityById(int jobId)
        {
            var job = await _studentService.GetJobOpportunityByIdAsync(jobId);
            if (job == null)
                return NotFound(new { Message = "Job opportunity not found" });

            return Ok(job);
        }

        [HttpPost("jobs/apply")]
        public async Task<IActionResult> ApplyForJob([FromBody] JobApplicationDto applicationDto)
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var result = await _studentService.ApplyForJobAsync(studentId, applicationDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("jobs/applications")]
        public async Task<IActionResult> GetStudentApplications()
        {
            var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
            var applications = await _studentService.GetStudentApplicationsAsync(studentId);
            return Ok(applications);
        }

        [HttpPut("jobs/applications/{applicationId}")]
        public async Task<IActionResult> UpdateJobApplication(int applicationId, [FromBody] JobApplicationDto applicationDto)
        {
            try
            {
                var result = await _studentService.UpdateJobApplicationAsync(applicationId, applicationDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Dashboard
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            try
            {
                var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
                var dashboard = await _studentService.GetStudentDashboardAsync(studentId);
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("certificates")]
        public async Task<IActionResult> GetCertificates()
        {
            var studentId = int.Parse(User.FindFirst("StudentId")?.Value ?? "1");
            var certificates = await _studentService.GetStudentCertificatesAsync(studentId);
            return Ok(certificates);
        }
        #endregion
    }
}