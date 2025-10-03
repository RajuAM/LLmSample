using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementLMS.DTOs.Feedback;
using PlacementLMS.Services.Feedback;

namespace PlacementLMS.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        #region Feedback Management
        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackDto feedbackDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { Message = "User not authenticated" });

                var feedback = await _feedbackService.CreateFeedbackAsync(userId, feedbackDto);
                return CreatedAtAction(nameof(GetFeedbackById), new { feedbackId = feedback.Id }, feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{feedbackId}")]
        public async Task<IActionResult> GetFeedbackById(int feedbackId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { Message = "User not authenticated" });

                var canView = await _feedbackService.CanUserViewFeedbackAsync(userId, feedbackId);
                if (!canView)
                    return Forbid();

                var feedback = await _feedbackService.GetFeedbackByIdAsync(feedbackId);
                if (feedback == null)
                    return NotFound(new { Message = "Feedback not found" });

                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("received")]
        public async Task<IActionResult> GetFeedbackForUser([FromQuery] string feedbackType = null)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { Message = "User not authenticated" });

                var feedbacks = await _feedbackService.GetFeedbackForUserAsync(userId, feedbackType);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("given")]
        public async Task<IActionResult> GetFeedbackByUser([FromQuery] string feedbackType = null)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { Message = "User not authenticated" });

                var feedbacks = await _feedbackService.GetFeedbackByUserAsync(userId, feedbackType);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Admin,Company,Institution")]
        public async Task<IActionResult> GetFeedbackForStudent(int studentId)
        {
            try
            {
                var feedbacks = await _feedbackService.GetFeedbackForStudentAsync(studentId);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("company/{companyId}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetFeedbackForCompany(int companyId)
        {
            try
            {
                var feedbacks = await _feedbackService.GetFeedbackForCompanyAsync(companyId);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("course/{courseId}")]
        [Authorize(Roles = "Admin,Institution,Student")]
        public async Task<IActionResult> GetFeedbackForCourse(int courseId)
        {
            try
            {
                var feedbacks = await _feedbackService.GetFeedbackForCourseAsync(courseId);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{feedbackId}")]
        public async Task<IActionResult> UpdateFeedback(int feedbackId, [FromBody] UpdateFeedbackDto feedbackDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { Message = "User not authenticated" });

                var canEdit = await _feedbackService.CanUserEditFeedbackAsync(userId, feedbackId);
                if (!canEdit)
                    return Forbid();

                await _feedbackService.UpdateFeedbackAsync(feedbackId, feedbackDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{feedbackId}")]
        public async Task<IActionResult> DeleteFeedback(int feedbackId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { Message = "User not authenticated" });

                var canEdit = await _feedbackService.CanUserEditFeedbackAsync(userId, feedbackId);
                if (!canEdit)
                    return Forbid();

                await _feedbackService.DeleteFeedbackAsync(feedbackId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("recent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRecentFeedback([FromQuery] int count = 10)
        {
            try
            {
                var feedbacks = await _feedbackService.GetRecentFeedbackAsync(count);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Analytics
        [HttpGet("analytics")]
        [Authorize(Roles = "Admin,Company,Institution")]
        public async Task<IActionResult> GetFeedbackAnalytics([FromQuery] int? userId, [FromQuery] int? companyId, [FromQuery] int? courseId)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

                // Users can only view analytics for their own data unless they're admin
                if (User.FindFirst("UserType")?.Value != "Admin")
                {
                    if (userId.HasValue && userId.Value != currentUserId)
                        return Forbid();

                    if (!userId.HasValue)
                        userId = currentUserId;
                }

                var analytics = await _feedbackService.GetFeedbackAnalyticsAsync(userId, companyId, courseId);
                return Ok(analytics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("rating/{entityType}/{entityId}")]
        [Authorize]
        public async Task<IActionResult> GetAverageRating(string entityType, int entityId)
        {
            try
            {
                var rating = await _feedbackService.GetAverageRatingAsync(entityId, entityType);
                return Ok(new { EntityType = entityType, EntityId = entityId, AverageRating = rating });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion
    }
}