using PlacementLMS.DTOs.Course;
using PlacementLMS.DTOs.MockTest;
using PlacementLMS.DTOs.Placement;
using PlacementLMS.DTOs.Resume;
using PlacementLMS.DTOs.Student;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Student
{
    public class StudentService : IStudentService
    {
        // In a real application, you would inject a database context here
        private readonly List<Models.Student> _students = new List<Models.Student>();
        private readonly List<StudentCourse> _studentCourses = new List<StudentCourse>();
        private readonly List<Models.Course> _courses = new List<Models.Course>();
        private int _nextStudentId = 1;

        public StudentService()
        {
            InitializeSampleData();
        }

        #region Profile Management
        public async Task<Models.Student> CompleteStudentProfileAsync(int userId, StudentRegistrationDto profileDto)
        {
            var student = new Models.Student
            {
                Id = _nextStudentId++,
                UserId = userId,
                StudentId = profileDto.StudentId,
                UniversityName = profileDto.UniversityName,
                Department = profileDto.Department,
                CurrentSemester = profileDto.CurrentSemester,
                CGPA = profileDto.CGPA,
                GraduationDate = profileDto.GraduationDate,
                ResumeFilePath = profileDto.ResumeFilePath,
                IsProfileComplete = true,
                IsVerified = false,
                EnrollmentDate = DateTime.UtcNow
            };

            _students.Add(student);
            return student;
        }

        public async Task<StudentResponseDto> GetStudentProfileAsync(int studentId)
        {
            var student = _students.FirstOrDefault(s => s.Id == studentId);
            if (student == null) return null;

            return new StudentResponseDto
            {
                Id = student.Id,
                FirstName = "Sample", // Would be fetched from User table
                LastName = "Student",
                Email = "student@example.com",
                PhoneNumber = "1234567890",
                StudentId = student.StudentId,
                UniversityName = student.UniversityName,
                Department = student.Department,
                CurrentSemester = student.CurrentSemester,
                CGPA = student.CGPA,
                ResumeFilePath = student.ResumeFilePath,
                IsProfileComplete = student.IsProfileComplete,
                IsVerified = student.IsVerified,
                EnrollmentDate = student.EnrollmentDate,
                GraduationDate = student.GraduationDate,
                EnrolledCourses = new List<StudentCourseDto>(),
                Certificates = new List<CertificateDto>()
            };
        }

        public async Task<StudentResponseDto> UpdateStudentProfileAsync(int studentId, StudentProfileDto profileDto)
        {
            var student = _students.FirstOrDefault(s => s.Id == studentId);
            if (student == null) return null;

            // Update student properties from profileDto
            student.CurrentSemester = profileDto.CurrentSemester;
            student.CGPA = profileDto.CGPA;
            student.ResumeFilePath = profileDto.ResumeFilePath;

            return await GetStudentProfileAsync(studentId);
        }
        #endregion

        #region Course Management
        public async Task<IEnumerable<CourseResponseDto>> GetAvailableCoursesAsync()
        {
            return _courses.Where(c => c.IsActive).Select(c => new CourseResponseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                InstitutionName = "Sample Institution",
                Category = c.Category,
                Price = c.Price,
                DiscountPrice = c.DiscountPrice,
                DurationHours = c.DurationHours,
                ThumbnailImagePath = c.ThumbnailImagePath,
                VideoPath = c.VideoPath,
                PDFMaterialPath = c.PDFMaterialPath,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                EnrollmentCount = 0
            });
        }

        public async Task<CourseResponseDto> GetCourseByIdAsync(int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId && c.IsActive);
            if (course == null) return null;

            return new CourseResponseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                InstitutionName = "Sample Institution",
                Category = course.Category,
                Price = course.Price,
                DiscountPrice = course.DiscountPrice,
                DurationHours = course.DurationHours,
                ThumbnailImagePath = course.ThumbnailImagePath,
                VideoPath = course.VideoPath,
                PDFMaterialPath = course.PDFMaterialPath,
                IsActive = course.IsActive,
                CreatedAt = course.CreatedAt,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                EnrollmentCount = 0
            };
        }

        public async Task<StudentCourse> EnrollInCourseAsync(int studentId, CourseEnrollmentDto enrollmentDto)
        {
            var enrollment = new StudentCourse
            {
                Id = _studentCourses.Count + 1,
                StudentId = studentId,
                CourseId = enrollmentDto.CourseId,
                EnrollmentDate = DateTime.UtcNow,
                AmountPaid = enrollmentDto.AmountPaid,
                PaymentStatus = enrollmentDto.PaymentStatus,
                PaymentTransactionId = enrollmentDto.PaymentTransactionId
            };

            _studentCourses.Add(enrollment);
            return enrollment;
        }

        public async Task<IEnumerable<StudentCourseDto>> GetEnrolledCoursesAsync(int studentId)
        {
            return _studentCourses.Where(sc => sc.StudentId == studentId)
                .Select(sc => new StudentCourseDto
                {
                    CourseId = sc.CourseId,
                    CourseTitle = "Sample Course", // Would be fetched from Course table
                    CourseCategory = "Technical",
                    EnrollmentDate = sc.EnrollmentDate,
                    CompletionDate = sc.CompletionDate,
                    IsCompleted = sc.IsCompleted,
                    ProgressPercentage = sc.ProgressPercentage,
                    PaymentStatus = sc.PaymentStatus,
                    AmountPaid = sc.AmountPaid
                });
        }

        public async Task<StudentCourseDto> GetCourseProgressAsync(int studentId, int courseId)
        {
            var progress = _studentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            if (progress == null) return null;

            return new StudentCourseDto
            {
                CourseId = progress.CourseId,
                CourseTitle = "Sample Course",
                CourseCategory = "Technical",
                EnrollmentDate = progress.EnrollmentDate,
                CompletionDate = progress.CompletionDate,
                IsCompleted = progress.IsCompleted,
                ProgressPercentage = progress.ProgressPercentage,
                PaymentStatus = progress.PaymentStatus,
                AmountPaid = progress.AmountPaid
            };
        }
        #endregion

        #region Assignment Management
        public async Task<IEnumerable<AssignmentResponseDto>> GetCourseAssignmentsAsync(int courseId)
        {
            // Return sample assignments for the course
            return new List<AssignmentResponseDto>
            {
                new AssignmentResponseDto
                {
                    Id = 1,
                    Title = "Introduction to Programming",
                    Description = "Basic programming concepts",
                    CourseTitle = "Programming Fundamentals",
                    DueDate = DateTime.UtcNow.AddDays(7),
                    MaxPoints = 100,
                    Instructions = "Complete all exercises",
                    AttachmentPath = "/files/assignment1.pdf",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
        }

        public async Task<StudentAssignmentResponseDto> SubmitAssignmentAsync(int studentId, StudentAssignmentDto submissionDto)
        {
            return new StudentAssignmentResponseDto
            {
                Id = 1,
                AssignmentTitle = "Sample Assignment",
                SubmittedAt = DateTime.UtcNow,
                SubmissionFilePath = submissionDto.SubmissionFilePath,
                SubmissionText = submissionDto.SubmissionText,
                PointsScored = null,
                Feedback = null,
                Status = "Submitted",
                GradedAt = null
            };
        }

        public async Task<IEnumerable<StudentAssignmentResponseDto>> GetStudentAssignmentsAsync(int studentId)
        {
            return new List<StudentAssignmentResponseDto>
            {
                new StudentAssignmentResponseDto
                {
                    Id = 1,
                    AssignmentTitle = "Programming Assignment",
                    SubmittedAt = DateTime.UtcNow.AddDays(-2),
                    SubmissionFilePath = "/submissions/assignment1.pdf",
                    SubmissionText = "My solution...",
                    PointsScored = 85,
                    Feedback = "Good work!",
                    Status = "Graded",
                    GradedAt = DateTime.UtcNow.AddDays(-1)
                }
            };
        }
        #endregion

        #region Mock Test Management
        public async Task<IEnumerable<MockTestResponseDto>> GetAvailableMockTestsAsync()
        {
            return new List<MockTestResponseDto>
            {
                new MockTestResponseDto
                {
                    Id = 1,
                    Title = "Programming Fundamentals Test",
                    Description = "Test your programming knowledge",
                    InstitutionName = "Sample Institution",
                    Subject = "Computer Science",
                    DurationMinutes = 60,
                    TotalQuestions = 30,
                    MaxMarks = 100,
                    Instructions = "Answer all questions",
                    IsActive = true,
                    ScheduledDate = DateTime.UtcNow.AddDays(1),
                    CreatedAt = DateTime.UtcNow,
                    ParticipantCount = 25
                }
            };
        }

        public async Task<MockTestResponseDto> GetMockTestByIdAsync(int testId)
        {
            return new MockTestResponseDto
            {
                Id = testId,
                Title = "Sample Mock Test",
                Description = "Test description",
                InstitutionName = "Sample Institution",
                Subject = "Computer Science",
                DurationMinutes = 60,
                TotalQuestions = 30,
                MaxMarks = 100,
                Instructions = "Answer all questions",
                IsActive = true,
                ScheduledDate = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow,
                ParticipantCount = 25
            };
        }

        public async Task<StudentMockTestResponseDto> StartMockTestAsync(int studentId, int testId)
        {
            return new StudentMockTestResponseDto
            {
                Id = 1,
                MockTestTitle = "Sample Test",
                StartedAt = DateTime.UtcNow,
                CompletedAt = null,
                Score = 0,
                MaxScore = 100,
                Percentage = 0,
                Status = "In Progress",
                TimeTaken = TimeSpan.Zero
            };
        }

        public async Task<TestResultDto> SubmitMockTestAsync(int studentId, int testId, StudentMockTestDto submissionDto)
        {
            var score = CalculateTestScore(submissionDto.Answers);

            return new TestResultDto
            {
                MockTestId = testId,
                MockTestTitle = "Sample Test",
                Score = score,
                MaxScore = 100,
                Percentage = (score / 100.0) * 100,
                Grade = GetGrade(score),
                CompletedAt = DateTime.UtcNow
            };
        }

        public async Task<IEnumerable<TestResultDto>> GetStudentTestResultsAsync(int studentId)
        {
            return new List<TestResultDto>
            {
                new TestResultDto
                {
                    MockTestId = 1,
                    MockTestTitle = "Programming Test",
                    Score = 85,
                    MaxScore = 100,
                    Percentage = 85,
                    Grade = "A",
                    CompletedAt = DateTime.UtcNow.AddDays(-3)
                }
            };
        }
        #endregion

        #region Resume Management
        public async Task<ResumeResponseDto> CreateOrUpdateResumeAsync(int studentId, ResumeDto resumeDto)
        {
            return new ResumeResponseDto
            {
                Id = 1,
                StudentId = studentId,
                StudentName = "Sample Student",
                Objective = resumeDto.Objective,
                Skills = resumeDto.Skills,
                Education = resumeDto.Education,
                Experience = resumeDto.Experience,
                Projects = resumeDto.Projects,
                Certifications = resumeDto.Certifications,
                ResumeFilePath = resumeDto.ResumeFilePath,
                IsPublic = resumeDto.IsPublic,
                LastUpdated = DateTime.UtcNow,
                ResumeScore = 85.5,
                Suggestions = new List<string> { "Add more projects", "Include relevant certifications" }
            };
        }

        public async Task<ResumeResponseDto> GetStudentResumeAsync(int studentId)
        {
            return new ResumeResponseDto
            {
                Id = 1,
                StudentId = studentId,
                StudentName = "Sample Student",
                Objective = "To obtain a challenging position in software development",
                Skills = "C#, JavaScript, SQL",
                Education = new List<EducationDto>
                {
                    new EducationDto
                    {
                        Degree = "Bachelor of Technology",
                        Institution = "Sample University",
                        StartDate = DateTime.UtcNow.AddYears(-4),
                        EndDate = DateTime.UtcNow.AddYears(-1),
                        GPA = 8.5
                    }
                },
                Experience = new List<ExperienceDto>(),
                Projects = new List<ProjectDto>(),
                Certifications = new List<CertificationDto>(),
                ResumeFilePath = "/resumes/sample_resume.pdf",
                IsPublic = false,
                LastUpdated = DateTime.UtcNow,
                ResumeScore = 85.5,
                Suggestions = new List<string>()
            };
        }

        public async Task<IEnumerable<ResumeTemplateDto>> GetResumeTemplatesAsync()
        {
            return new List<ResumeTemplateDto>
            {
                new ResumeTemplateDto
                {
                    TemplateName = "Modern Template",
                    Industry = "Technology",
                    TemplateContent = "Modern resume template content...",
                    PreviewImagePath = "/templates/modern_preview.png",
                    IsActive = true
                },
                new ResumeTemplateDto
                {
                    TemplateName = "Classic Template",
                    Industry = "General",
                    TemplateContent = "Classic resume template content...",
                    PreviewImagePath = "/templates/classic_preview.png",
                    IsActive = true
                }
            };
        }

        public async Task<ResumeResponseDto> GenerateResumeFromTemplateAsync(int studentId, int templateId)
        {
            return await GetStudentResumeAsync(studentId);
        }
        #endregion

        #region Job Application Management
        public async Task<IEnumerable<JobOpportunityResponseDto>> GetAvailableJobOpportunitiesAsync(int studentId)
        {
            return new List<JobOpportunityResponseDto>
            {
                new JobOpportunityResponseDto
                {
                    Id = 1,
                    Title = "Software Developer",
                    Description = "Develop web applications",
                    CompanyName = "Tech Corp",
                    JobType = "Full-time",
                    ExperienceLevel = "Entry",
                    Location = "New York",
                    SalaryMin = 50000,
                    SalaryMax = 70000,
                    SkillsRequired = "C#, JavaScript, SQL",
                    Responsibilities = "Develop and maintain web applications",
                    Benefits = "Health insurance, 401k",
                    NumberOfPositions = 5,
                    NumberOfApplications = 25,
                    ApplicationDeadline = DateTime.UtcNow.AddDays(30),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
        }

        public async Task<JobOpportunityResponseDto> GetJobOpportunityByIdAsync(int jobId)
        {
            return new JobOpportunityResponseDto
            {
                Id = jobId,
                Title = "Sample Job",
                Description = "Job description",
                CompanyName = "Sample Company",
                JobType = "Full-time",
                ExperienceLevel = "Entry",
                Location = "Sample Location",
                SalaryMin = 50000,
                SalaryMax = 70000,
                SkillsRequired = "Sample skills",
                NumberOfPositions = 1,
                NumberOfApplications = 0,
                ApplicationDeadline = DateTime.UtcNow.AddDays(30),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }

        public async Task<JobApplicationResponseDto> ApplyForJobAsync(int studentId, JobApplicationDto applicationDto)
        {
            return new JobApplicationResponseDto
            {
                Id = 1,
                JobTitle = "Sample Job",
                CompanyName = "Sample Company",
                StudentName = "Sample Student",
                AppliedAt = DateTime.UtcNow,
                Status = "Applied",
                CoverLetter = applicationDto.CoverLetter
            };
        }

        public async Task<IEnumerable<JobApplicationResponseDto>> GetStudentApplicationsAsync(int studentId)
        {
            return new List<JobApplicationResponseDto>
            {
                new JobApplicationResponseDto
                {
                    Id = 1,
                    JobTitle = "Software Developer",
                    CompanyName = "Tech Corp",
                    StudentName = "Sample Student",
                    AppliedAt = DateTime.UtcNow.AddDays(-5),
                    Status = "Under Review",
                    CoverLetter = "I am interested in this position..."
                }
            };
        }

        public async Task<JobApplicationResponseDto> UpdateJobApplicationAsync(int applicationId, JobApplicationDto applicationDto)
        {
            return new JobApplicationResponseDto
            {
                Id = applicationId,
                JobTitle = "Sample Job",
                CompanyName = "Sample Company",
                StudentName = "Sample Student",
                AppliedAt = DateTime.UtcNow,
                Status = "Updated",
                CoverLetter = applicationDto.CoverLetter
            };
        }
        #endregion

        #region Dashboard & Progress
        public async Task<StudentDashboardDto> GetStudentDashboardAsync(int studentId)
        {
            var profile = await GetStudentProfileAsync(studentId);
            var courses = await GetEnrolledCoursesAsync(studentId);
            var testResults = await GetStudentTestResultsAsync(studentId);
            var applications = await GetStudentApplicationsAsync(studentId);

            return new StudentDashboardDto
            {
                Profile = profile,
                RecentCourses = courses.Take(3).ToList(),
                RecentTestResults = testResults.Take(3).ToList(),
                RecentApplications = applications.Take(3).ToList(),
                TotalCoursesEnrolled = courses.Count(),
                TotalCertificatesEarned = 0,
                TotalJobApplications = applications.Count(),
                AverageTestScore = testResults.Any() ? testResults.Average(tr => tr.Percentage) : 0,
                UpcomingDeadlines = new List<string> { "Assignment due in 3 days", "Mock test tomorrow" }
            };
        }

        public async Task<IEnumerable<CertificateDto>> GetStudentCertificatesAsync(int studentId)
        {
            return new List<CertificateDto>
            {
                new CertificateDto
                {
                    Id = 1,
                    CertificateNumber = "CERT-2024-001",
                    CourseName = "Programming Fundamentals",
                    IssueDate = DateTime.UtcNow.AddDays(-30),
                    ExpiryDate = DateTime.UtcNow.AddYears(2),
                    CertificateFilePath = "/certificates/cert_001.pdf"
                }
            };
        }
        #endregion

        private void InitializeSampleData()
        {
            // Initialize sample courses
            _courses.Add(new Models.Course
            {
                Id = 1,
                Title = "Programming Fundamentals",
                Description = "Learn basic programming concepts",
                InstitutionId = 1,
                Category = "Technical",
                Price = 99.99m,
                DurationHours = 40,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(3)
            });
        }

        private int CalculateTestScore(List<StudentTestAnswerDto> answers)
        {
            // Simple scoring logic - in real implementation, compare with correct answers
            return new Random().Next(60, 100);
        }

        private string GetGrade(int score)
        {
            if (score >= 90) return "A";
            if (score >= 80) return "B";
            if (score >= 70) return "C";
            if (score >= 60) return "D";
            return "F";
        }
    }
}