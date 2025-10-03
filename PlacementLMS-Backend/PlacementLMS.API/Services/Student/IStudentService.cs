using PlacementLMS.DTOs.Course;
using PlacementLMS.DTOs.MockTest;
using PlacementLMS.DTOs.Placement;
using PlacementLMS.DTOs.Resume;
using PlacementLMS.DTOs.Student;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Student
{
    public interface IStudentService
    {
        // Profile Management
        Task<Models.Student> CompleteStudentProfileAsync(int userId, StudentRegistrationDto profileDto);
        Task<StudentResponseDto> GetStudentProfileAsync(int studentId);
        Task<StudentResponseDto> UpdateStudentProfileAsync(int studentId, StudentProfileDto profileDto);

        // Course Management
        Task<IEnumerable<CourseResponseDto>> GetAvailableCoursesAsync();
        Task<CourseResponseDto> GetCourseByIdAsync(int courseId);
        Task<StudentCourse> EnrollInCourseAsync(int studentId, CourseEnrollmentDto enrollmentDto);
        Task<IEnumerable<StudentCourseDto>> GetEnrolledCoursesAsync(int studentId);
        Task<StudentCourseDto> GetCourseProgressAsync(int studentId, int courseId);

        // Assignment Management
        Task<IEnumerable<AssignmentResponseDto>> GetCourseAssignmentsAsync(int courseId);
        Task<StudentAssignmentResponseDto> SubmitAssignmentAsync(int studentId, StudentAssignmentDto submissionDto);
        Task<IEnumerable<StudentAssignmentResponseDto>> GetStudentAssignmentsAsync(int studentId);

        // Mock Test Management
        Task<IEnumerable<MockTestResponseDto>> GetAvailableMockTestsAsync();
        Task<MockTestResponseDto> GetMockTestByIdAsync(int testId);
        Task<StudentMockTestResponseDto> StartMockTestAsync(int studentId, int testId);
        Task<TestResultDto> SubmitMockTestAsync(int studentId, int testId, StudentMockTestDto submissionDto);
        Task<IEnumerable<TestResultDto>> GetStudentTestResultsAsync(int studentId);

        // Resume Management
        Task<ResumeResponseDto> CreateOrUpdateResumeAsync(int studentId, ResumeDto resumeDto);
        Task<ResumeResponseDto> GetStudentResumeAsync(int studentId);
        Task<IEnumerable<ResumeTemplateDto>> GetResumeTemplatesAsync();
        Task<ResumeResponseDto> GenerateResumeFromTemplateAsync(int studentId, int templateId);

        // Job Application Management
        Task<IEnumerable<JobOpportunityResponseDto>> GetAvailableJobOpportunitiesAsync(int studentId);
        Task<JobOpportunityResponseDto> GetJobOpportunityByIdAsync(int jobId);
        Task<JobApplicationResponseDto> ApplyForJobAsync(int studentId, JobApplicationDto applicationDto);
        Task<IEnumerable<JobApplicationResponseDto>> GetStudentApplicationsAsync(int studentId);
        Task<JobApplicationResponseDto> UpdateJobApplicationAsync(int applicationId, JobApplicationDto applicationDto);

        // Dashboard & Progress
        Task<StudentDashboardDto> GetStudentDashboardAsync(int studentId);
        Task<IEnumerable<CertificateDto>> GetStudentCertificatesAsync(int studentId);
    }

    public class StudentDashboardDto
    {
        public StudentResponseDto Profile { get; set; }
        public List<StudentCourseDto> RecentCourses { get; set; } = new List<StudentCourseDto>();
        public List<TestResultDto> RecentTestResults { get; set; } = new List<TestResultDto>();
        public List<JobApplicationResponseDto> RecentApplications { get; set; } = new List<JobApplicationResponseDto>();
        public int TotalCoursesEnrolled { get; set; }
        public int TotalCertificatesEarned { get; set; }
        public int TotalJobApplications { get; set; }
        public double AverageTestScore { get; set; }
        public List<string> UpcomingDeadlines { get; set; } = new List<string>();
    }
}