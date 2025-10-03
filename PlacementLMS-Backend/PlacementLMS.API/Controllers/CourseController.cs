using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementLMS.DTOs.Course;
using PlacementLMS.Services.Course;

namespace PlacementLMS.Controllers
{
    [ApiController]
    [Route("api/course")]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #region Course Management
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);
            if (course == null)
                return NotFound(new { Message = "Course not found" });

            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Institution")]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDto courseDto)
        {
            try
            {
                var course = await _courseService.CreateCourseAsync(courseDto);
                return CreatedAtAction(nameof(GetCourseById), new { courseId = course.Id }, course);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{courseId}")]
        [Authorize(Roles = "Admin,Institution")]
        public async Task<IActionResult> UpdateCourse(int courseId, [FromBody] CourseDto courseDto)
        {
            try
            {
                await _courseService.UpdateCourseAsync(courseId, courseDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{courseId}")]
        [Authorize(Roles = "Admin,Institution")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            try
            {
                await _courseService.DeleteCourseAsync(courseId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("institution/{institutionId}")]
        public async Task<IActionResult> GetCoursesByInstitution(int institutionId)
        {
            var courses = await _courseService.GetCoursesByInstitutionAsync(institutionId);
            return Ok(courses);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetCoursesByCategory(string category)
        {
            var courses = await _courseService.GetCoursesByCategoryAsync(category);
            return Ok(courses);
        }
        #endregion

        #region Assignment Management
        [HttpGet("{courseId}/assignments")]
        public async Task<IActionResult> GetCourseAssignments(int courseId)
        {
            var assignments = await _courseService.GetCourseAssignmentsAsync(courseId);
            return Ok(assignments);
        }

        [HttpGet("assignments/{assignmentId}")]
        public async Task<IActionResult> GetAssignmentById(int assignmentId)
        {
            var assignment = await _courseService.GetAssignmentByIdAsync(assignmentId);
            if (assignment == null)
                return NotFound(new { Message = "Assignment not found" });

            return Ok(assignment);
        }

        [HttpPost("assignments")]
        [Authorize(Roles = "Admin,Institution")]
        public async Task<IActionResult> CreateAssignment([FromBody] AssignmentDto assignmentDto)
        {
            try
            {
                var assignment = await _courseService.CreateAssignmentAsync(assignmentDto);
                return CreatedAtAction(nameof(GetAssignmentById), new { assignmentId = assignment.Id }, assignment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("assignments/{assignmentId}")]
        [Authorize(Roles = "Admin,Institution")]
        public async Task<IActionResult> UpdateAssignment(int assignmentId, [FromBody] AssignmentDto assignmentDto)
        {
            try
            {
                await _courseService.UpdateAssignmentAsync(assignmentId, assignmentDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("assignments/{assignmentId}")]
        [Authorize(Roles = "Admin,Institution")]
        public async Task<IActionResult> DeleteAssignment(int assignmentId)
        {
            try
            {
                await _courseService.DeleteAssignmentAsync(assignmentId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Content Management
        [HttpGet("{courseId}/content")]
        public async Task<IActionResult> GetCourseContent(int courseId)
        {
            var videoUrl = await _courseService.GetCourseContentUrlAsync(courseId, "video");
            var pdfUrl = await _courseService.GetCourseContentUrlAsync(courseId, "pdf");

            return Ok(new
            {
                CourseId = courseId,
                VideoUrl = videoUrl,
                PdfUrl = pdfUrl
            });
        }

        [HttpPut("{courseId}/content")]
        [Authorize(Roles = "Admin,Institution")]
        public async Task<IActionResult> UpdateCourseContent(int courseId, [FromBody] UpdateCourseContentDto contentDto)
        {
            try
            {
                await _courseService.UpdateCourseContentAsync(courseId, contentDto.VideoPath, contentDto.PdfPath);
                return Ok(new { Message = "Course content updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Analytics
        [HttpGet("{courseId}/analytics")]
        [Authorize(Roles = "Admin,Institution")]
        public async Task<IActionResult> GetCourseAnalytics(int courseId)
        {
            var analytics = await _courseService.GetCourseAnalyticsAsync(courseId);
            return Ok(analytics);
        }

        [HttpGet("{courseId}/enrollments")]
        [Authorize(Roles = "Admin,Institution")]
        public async Task<IActionResult> GetCourseEnrollments(int courseId)
        {
            var enrollments = await _courseService.GetCourseEnrollmentsAsync(courseId);
            return Ok(enrollments);
        }
        #endregion
    }

    public class UpdateCourseContentDto
    {
        public string VideoPath { get; set; }
        public string PdfPath { get; set; }
    }
}