using PlacementLMS.DTOs.Course;
using PlacementLMS.DTOs.Student;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Course
{
    public interface ICourseService
    {
        // Course Management
        Task<Models.Course> CreateCourseAsync(CourseDto courseDto);
        Task<IEnumerable<CourseResponseDto>> GetAllCoursesAsync();
        Task<CourseResponseDto> GetCourseByIdAsync(int courseId);
        Task UpdateCourseAsync(int courseId, CourseDto courseDto);
        Task DeleteCourseAsync(int courseId);
        Task<IEnumerable<CourseResponseDto>> GetCoursesByInstitutionAsync(int institutionId);
        Task<IEnumerable<CourseResponseDto>> GetCoursesByCategoryAsync(string category);

        // Assignment Management
        Task<Assignment> CreateAssignmentAsync(AssignmentDto assignmentDto);
        Task<IEnumerable<AssignmentResponseDto>> GetCourseAssignmentsAsync(int courseId);
        Task<AssignmentResponseDto> GetAssignmentByIdAsync(int assignmentId);
        Task UpdateAssignmentAsync(int assignmentId, AssignmentDto assignmentDto);
        Task DeleteAssignmentAsync(int assignmentId);
        Task<AssignmentResponseDto> GradeAssignmentAsync(int assignmentId, int studentId, int points, string feedback);

        // Enrollment Management
        Task<IEnumerable<StudentCourseDto>> GetCourseEnrollmentsAsync(int courseId);
        Task<bool> IsStudentEnrolledAsync(int studentId, int courseId);
        Task<double> GetCourseProgressAsync(int studentId, int courseId);
        Task MarkCourseCompletedAsync(int studentId, int courseId);

        // Content Management
        Task<Models.Course> UpdateCourseContentAsync(int courseId, string videoPath, string pdfPath);
        Task<string> GetCourseContentUrlAsync(int courseId, string contentType);

        // Analytics
        Task<CourseAnalyticsDto> GetCourseAnalyticsAsync(int courseId);
    }

    public class CourseAnalyticsDto
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int TotalEnrollments { get; set; }
        public int ActiveStudents { get; set; }
        public int CompletedStudents { get; set; }
        public double CompletionRate { get; set; }
        public double AverageProgress { get; set; }
        public double AverageAssignmentScore { get; set; }
        public List<WeeklyProgressDto> WeeklyProgress { get; set; } = new List<WeeklyProgressDto>();
    }

    public class WeeklyProgressDto
    {
        public DateTime Week { get; set; }
        public int NewEnrollments { get; set; }
        public int Completions { get; set; }
        public double AverageProgress { get; set; }
    }
}