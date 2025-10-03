using PlacementLMS.DTOs.Feedback;
using PlacementLMS.Models;
using FeedbackModel = PlacementLMS.Models.Feedback;

namespace PlacementLMS.Services.Feedback
{
    public interface IFeedbackService
    {
        Task<FeedbackModel> CreateFeedbackAsync(int fromUserId, CreateFeedbackDto feedbackDto);
        Task<FeedbackModel> GetFeedbackByIdAsync(int feedbackId);
        Task<IEnumerable<FeedbackDto>> GetFeedbackForUserAsync(int userId, string feedbackType = null);
        Task<IEnumerable<FeedbackDto>> GetFeedbackByUserAsync(int userId, string feedbackType = null);
        Task<IEnumerable<FeedbackDto>> GetFeedbackForStudentAsync(int studentId);
        Task<IEnumerable<FeedbackDto>> GetFeedbackForCompanyAsync(int companyId);
        Task<IEnumerable<FeedbackDto>> GetFeedbackForCourseAsync(int courseId);
        Task UpdateFeedbackAsync(int feedbackId, UpdateFeedbackDto feedbackDto);
        Task DeleteFeedbackAsync(int feedbackId);
        Task<FeedbackAnalyticsDto> GetFeedbackAnalyticsAsync(int? userId = null, int? companyId = null, int? courseId = null);
        Task<double> GetAverageRatingAsync(int entityId, string entityType); // entityType: Student, Company, Course
        Task<IEnumerable<FeedbackDto>> GetRecentFeedbackAsync(int count = 10);
        Task<bool> CanUserViewFeedbackAsync(int userId, int feedbackId);
        Task<bool> CanUserEditFeedbackAsync(int userId, int feedbackId);
    }
}