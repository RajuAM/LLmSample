using PlacementLMS.DTOs.Feedback;
using PlacementLMS.Models;
using AssessmentModel = PlacementLMS.Models.Assessment;

namespace PlacementLMS.Services.Feedback
{
    public interface IAssessmentService
    {
        Task<AssessmentModel> CreateAssessmentAsync(int createdByUserId, CreateAssessmentDto assessmentDto);
        Task<AssessmentModel> GetAssessmentByIdAsync(int assessmentId);
        Task<IEnumerable<AssessmentModel>> GetAssessmentsForUserAsync(int userId);
        Task<IEnumerable<AssessmentModel>> GetAssessmentsByTypeAsync(string assessmentType);
        Task<IEnumerable<AssessmentModel>> GetActiveAssessmentsAsync();
        Task UpdateAssessmentAsync(int assessmentId, CreateAssessmentDto assessmentDto);
        Task DeleteAssessmentAsync(int assessmentId);
        Task<AssessmentModel> AssignAssessmentToStudentAsync(int assessmentId, int studentId);
        Task<StudentAssessment> StartAssessmentAsync(int studentAssessmentId);
        Task<StudentAssessment> SubmitAssessmentAsync(int studentAssessmentId, Dictionary<int, string> answers);
        Task<StudentAssessment> GetStudentAssessmentAsync(int studentId, int assessmentId);
        Task<IEnumerable<StudentAssessment>> GetAssessmentResultsAsync(int assessmentId);
        Task<AssessmentAnalyticsDto> GetAssessmentAnalyticsAsync(int assessmentId);
        Task<bool> CanUserAccessAssessmentAsync(int userId, int assessmentId);
    }

    public class AssessmentAnalyticsDto
    {
        public int TotalAttempts { get; set; }
        public double AverageScore { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
        public double PassRate { get; set; }
        public Dictionary<string, double> ScoreDistribution { get; set; }
        public List<QuestionAnalyticsDto> QuestionAnalytics { get; set; }
    }

    public class QuestionAnalyticsDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public double AverageScore { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAttempts { get; set; }
    }
}