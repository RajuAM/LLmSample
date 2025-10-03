using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Feedback
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public string FromUserName { get; set; }
        public int ToUserId { get; set; }
        public string ToUserName { get; set; }
        public int? StudentId { get; set; }
        public string StudentName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int? JobApplicationId { get; set; }
        public int? TrainingSessionId { get; set; }
        public string FeedbackType { get; set; }
        public string Category { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public string Strengths { get; set; }
        public string AreasForImprovement { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}