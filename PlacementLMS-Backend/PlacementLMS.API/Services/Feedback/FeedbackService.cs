using Microsoft.EntityFrameworkCore;
using PlacementLMS.Data;
using PlacementLMS.DTOs.Feedback;
using PlacementLMS.Models;
using FeedbackModel = PlacementLMS.Models.Feedback;

namespace PlacementLMS.Services.Feedback
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FeedbackModel> CreateFeedbackAsync(int fromUserId, CreateFeedbackDto feedbackDto)
        {
            var feedback = new FeedbackModel
            {
                FromUserId = fromUserId,
                ToUserId = feedbackDto.ToUserId,
                StudentId = feedbackDto.StudentId,
                CompanyId = feedbackDto.CompanyId,
                CourseId = feedbackDto.CourseId,
                JobApplicationId = feedbackDto.JobApplicationId,
                TrainingSessionId = feedbackDto.TrainingSessionId,
                FeedbackType = feedbackDto.FeedbackType,
                Category = feedbackDto.Category,
                Rating = feedbackDto.Rating,
                Comments = feedbackDto.Comments,
                Strengths = feedbackDto.Strengths,
                AreasForImprovement = feedbackDto.AreasForImprovement,
                IsAnonymous = feedbackDto.IsAnonymous,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        public async Task<FeedbackModel> GetFeedbackByIdAsync(int feedbackId)
        {
            return await _context.Feedbacks
                .Include(f => f.FromUser)
                .Include(f => f.ToUser)
                .Include(f => f.Student)
                .Include(f => f.Company)
                .Include(f => f.Course)
                .FirstOrDefaultAsync(f => f.Id == feedbackId);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbackForUserAsync(int userId, string feedbackType = null)
        {
            var query = _context.Feedbacks
                .Include(f => f.FromUser)
                .Include(f => f.Student)
                .Include(f => f.Company)
                .Include(f => f.Course)
                .Where(f => f.ToUserId == userId || f.StudentId.HasValue || f.CompanyId.HasValue || f.CourseId.HasValue);

            if (!string.IsNullOrEmpty(feedbackType))
            {
                query = query.Where(f => f.FeedbackType == feedbackType);
            }

            var feedbacks = await query.OrderByDescending(f => f.CreatedAt).ToListAsync();

            return feedbacks.Select(MapToDto);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbackByUserAsync(int userId, string feedbackType = null)
        {
            var query = _context.Feedbacks
                .Include(f => f.ToUser)
                .Include(f => f.Student)
                .Include(f => f.Company)
                .Include(f => f.Course)
                .Where(f => f.FromUserId == userId);

            if (!string.IsNullOrEmpty(feedbackType))
            {
                query = query.Where(f => f.FeedbackType == feedbackType);
            }

            var feedbacks = await query.OrderByDescending(f => f.CreatedAt).ToListAsync();

            return feedbacks.Select(MapToDto);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbackForStudentAsync(int studentId)
        {
            var feedbacks = await _context.Feedbacks
                .Include(f => f.FromUser)
                .Include(f => f.ToUser)
                .Include(f => f.Company)
                .Where(f => f.StudentId == studentId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            return feedbacks.Select(MapToDto);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbackForCompanyAsync(int companyId)
        {
            var feedbacks = await _context.Feedbacks
                .Include(f => f.FromUser)
                .Include(f => f.ToUser)
                .Include(f => f.Student)
                .Where(f => f.CompanyId == companyId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            return feedbacks.Select(MapToDto);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbackForCourseAsync(int courseId)
        {
            var feedbacks = await _context.Feedbacks
                .Include(f => f.FromUser)
                .Include(f => f.ToUser)
                .Include(f => f.Student)
                .Include(f => f.Company)
                .Where(f => f.CourseId == courseId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            return feedbacks.Select(MapToDto);
        }

        public async Task UpdateFeedbackAsync(int feedbackId, UpdateFeedbackDto feedbackDto)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback == null)
                throw new KeyNotFoundException("Feedback not found");

            feedback.Category = feedbackDto.Category;
            feedback.Rating = feedbackDto.Rating;
            feedback.Comments = feedbackDto.Comments;
            feedback.Strengths = feedbackDto.Strengths;
            feedback.AreasForImprovement = feedbackDto.AreasForImprovement;
            feedback.IsPublished = feedbackDto.IsPublished;
            feedback.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback == null)
                throw new KeyNotFoundException("Feedback not found");

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task<FeedbackAnalyticsDto> GetFeedbackAnalyticsAsync(int? userId = null, int? companyId = null, int? courseId = null)
        {
            var query = _context.Feedbacks.AsQueryable();

            if (userId.HasValue)
                query = query.Where(f => f.ToUserId == userId || f.FromUserId == userId);
            if (companyId.HasValue)
                query = query.Where(f => f.CompanyId == companyId);
            if (courseId.HasValue)
                query = query.Where(f => f.CourseId == courseId);

            var feedbacks = await query.ToListAsync();

            return new FeedbackAnalyticsDto
            {
                TotalFeedbackCount = feedbacks.Count,
                AverageRating = feedbacks.Any() ? feedbacks.Average(f => f.Rating) : 0,
                FiveStarCount = feedbacks.Count(f => f.Rating == 5),
                FourStarCount = feedbacks.Count(f => f.Rating == 4),
                ThreeStarCount = feedbacks.Count(f => f.Rating == 3),
                TwoStarCount = feedbacks.Count(f => f.Rating == 2),
                OneStarCount = feedbacks.Count(f => f.Rating == 1),
                FeedbackByCategory = feedbacks.GroupBy(f => f.Category)
                    .ToDictionary(g => g.Key, g => g.Count()),
                FeedbackByType = feedbacks.GroupBy(f => f.FeedbackType)
                    .ToDictionary(g => g.Key, g => g.Count()),
                MonthlyTrends = feedbacks.GroupBy(f => new { f.CreatedAt.Year, f.CreatedAt.Month })
                    .Select(g => new MonthlyFeedbackDto
                    {
                        Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                        Count = g.Count(),
                        AverageRating = g.Average(f => f.Rating)
                    })
                    .OrderBy(m => m.Month)
                    .ToList(),
                TopRatedStudents = await GetTopRatedEntities<Models.Student>(f => f.StudentId),
                TopRatedCompanies = await GetTopRatedEntities<Models.Company>(f => f.CompanyId),
                TopRatedCourses = await GetTopRatedEntities<Models.Course>(f => f.CourseId)
            };
        }

        public async Task<double> GetAverageRatingAsync(int entityId, string entityType)
        {
            var query = _context.Feedbacks.AsQueryable();

            switch (entityType.ToLower())
            {
                case "student":
                    query = query.Where(f => f.StudentId == entityId);
                    break;
                case "company":
                    query = query.Where(f => f.CompanyId == entityId);
                    break;
                case "course":
                    query = query.Where(f => f.CourseId == entityId);
                    break;
                default:
                    return 0;
            }

            var feedbacks = await query.ToListAsync();
            return feedbacks.Any() ? feedbacks.Average(f => f.Rating) : 0;
        }

        public async Task<IEnumerable<FeedbackDto>> GetRecentFeedbackAsync(int count = 10)
        {
            var feedbacks = await _context.Feedbacks
                .Include(f => f.FromUser)
                .Include(f => f.ToUser)
                .Include(f => f.Student)
                .Include(f => f.Company)
                .Include(f => f.Course)
                .OrderByDescending(f => f.CreatedAt)
                .Take(count)
                .ToListAsync();

            return feedbacks.Select(MapToDto);
        }

        public async Task<bool> CanUserViewFeedbackAsync(int userId, int feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback == null) return false;

            // Users can view feedback they gave or received, or public feedback
            return feedback.FromUserId == userId ||
                   feedback.ToUserId == userId ||
                   feedback.IsPublished ||
                   feedback.StudentId.HasValue ||
                   feedback.CompanyId.HasValue ||
                   feedback.CourseId.HasValue;
        }

        public async Task<bool> CanUserEditFeedbackAsync(int userId, int feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback == null) return false;

            // Only the user who created the feedback can edit it
            return feedback.FromUserId == userId;
        }

        private async Task<List<TopRatedEntityDto>> GetTopRatedEntities<TEntity>(Func<FeedbackModel, int?> getEntityId) where TEntity : class
        {
            var entityIds = _context.Feedbacks
                .Where(f => getEntityId(f) != null)
                .Select(f => getEntityId(f).Value)
                .Distinct();

            var topRated = new List<TopRatedEntityDto>();

            foreach (var entityId in entityIds)
            {
                var feedbacks = await _context.Feedbacks
                    .Where(f => getEntityId(f) == entityId)
                    .ToListAsync();

                if (feedbacks.Any())
                {
                    topRated.Add(new TopRatedEntityDto
                    {
                        Id = entityId,
                        Name = typeof(TEntity).Name, // This would need to be improved with actual entity names
                        AverageRating = feedbacks.Average(f => f.Rating),
                        FeedbackCount = feedbacks.Count
                    });
                }
            }

            return topRated.OrderByDescending(e => e.AverageRating).Take(10).ToList();
        }

        private FeedbackDto MapToDto(FeedbackModel feedback)
        {
            return new FeedbackDto
            {
                Id = feedback.Id,
                FromUserId = feedback.FromUserId,
                FromUserName = feedback.FromUser?.FirstName + " " + feedback.FromUser?.LastName,
                ToUserId = feedback.ToUserId,
                ToUserName = feedback.ToUser?.FirstName + " " + feedback.ToUser?.LastName,
                StudentId = feedback.StudentId,
                StudentName = feedback.Student?.User?.FirstName + " " + feedback.Student?.User?.LastName,
                CompanyId = feedback.CompanyId,
                CompanyName = feedback.Company?.Name,
                CourseId = feedback.CourseId,
                CourseTitle = feedback.Course?.Title,
                JobApplicationId = feedback.JobApplicationId,
                TrainingSessionId = feedback.TrainingSessionId,
                FeedbackType = feedback.FeedbackType,
                Category = feedback.Category,
                Rating = feedback.Rating,
                Comments = feedback.Comments,
                Strengths = feedback.Strengths,
                AreasForImprovement = feedback.AreasForImprovement,
                IsAnonymous = feedback.IsAnonymous,
                IsPublished = feedback.IsPublished,
                CreatedAt = feedback.CreatedAt,
                UpdatedAt = feedback.UpdatedAt
            };
        }
    }
}