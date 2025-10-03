using PlacementLMS.DTOs.Course;
using PlacementLMS.DTOs.Student;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Course
{
    public class CourseService : ICourseService
    {
        private readonly List<Models.Course> _courses = new List<Models.Course>();
        private readonly List<Assignment> _assignments = new List<Assignment>();
        private int _nextCourseId = 1;
        private int _nextAssignmentId = 1;

        public CourseService()
        {
            InitializeSampleData();
        }

        #region Course Management
        public async Task<Models.Course> CreateCourseAsync(CourseDto courseDto)
        {
            var course = new Models.Course
            {
                Id = _nextCourseId++,
                Title = courseDto.Title,
                Description = courseDto.Description,
                InstitutionId = courseDto.InstitutionId,
                Category = courseDto.Category,
                Price = courseDto.Price,
                DiscountPrice = courseDto.DiscountPrice,
                DurationHours = courseDto.DurationHours,
                ThumbnailImagePath = courseDto.ThumbnailImagePath,
                VideoPath = courseDto.VideoPath,
                PDFMaterialPath = courseDto.PDFMaterialPath,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                StartDate = courseDto.StartDate,
                EndDate = courseDto.EndDate
            };

            _courses.Add(course);
            return course;
        }

        public async Task<IEnumerable<CourseResponseDto>> GetAllCoursesAsync()
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

        public async Task UpdateCourseAsync(int courseId, CourseDto courseDto)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course != null)
            {
                course.Title = courseDto.Title;
                course.Description = courseDto.Description;
                course.Category = courseDto.Category;
                course.Price = courseDto.Price;
                course.DiscountPrice = courseDto.DiscountPrice;
                course.DurationHours = courseDto.DurationHours;
                course.ThumbnailImagePath = courseDto.ThumbnailImagePath;
                course.VideoPath = courseDto.VideoPath;
                course.PDFMaterialPath = courseDto.PDFMaterialPath;
                course.StartDate = courseDto.StartDate;
                course.EndDate = courseDto.EndDate;
            }
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course != null)
            {
                course.IsActive = false;
            }
        }

        public async Task<IEnumerable<CourseResponseDto>> GetCoursesByInstitutionAsync(int institutionId)
        {
            return _courses.Where(c => c.InstitutionId == institutionId && c.IsActive)
                .Select(c => new CourseResponseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    InstitutionName = "Sample Institution",
                    Category = c.Category,
                    Price = c.Price,
                    DiscountPrice = c.DiscountPrice,
                    DurationHours = c.DurationHours,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    EnrollmentCount = 0
                });
        }

        public async Task<IEnumerable<CourseResponseDto>> GetCoursesByCategoryAsync(string category)
        {
            return _courses.Where(c => c.Category == category && c.IsActive)
                .Select(c => new CourseResponseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    InstitutionName = "Sample Institution",
                    Category = c.Category,
                    Price = c.Price,
                    DiscountPrice = c.DiscountPrice,
                    DurationHours = c.DurationHours,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    EnrollmentCount = 0
                });
        }
        #endregion

        #region Assignment Management
        public async Task<Assignment> CreateAssignmentAsync(AssignmentDto assignmentDto)
        {
            var assignment = new Assignment
            {
                Id = _nextAssignmentId++,
                Title = assignmentDto.Title,
                Description = assignmentDto.Description,
                CourseId = assignmentDto.CourseId,
                DueDate = assignmentDto.DueDate,
                MaxPoints = assignmentDto.MaxPoints,
                Instructions = assignmentDto.Instructions,
                AttachmentPath = assignmentDto.AttachmentPath,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _assignments.Add(assignment);
            return assignment;
        }

        public async Task<IEnumerable<AssignmentResponseDto>> GetCourseAssignmentsAsync(int courseId)
        {
            return _assignments.Where(a => a.CourseId == courseId && a.IsActive)
                .Select(a => new AssignmentResponseDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    CourseTitle = "Sample Course",
                    DueDate = a.DueDate,
                    MaxPoints = a.MaxPoints,
                    Instructions = a.Instructions,
                    AttachmentPath = a.AttachmentPath,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt
                });
        }

        public async Task<AssignmentResponseDto> GetAssignmentByIdAsync(int assignmentId)
        {
            var assignment = _assignments.FirstOrDefault(a => a.Id == assignmentId && a.IsActive);
            if (assignment == null) return null;

            return new AssignmentResponseDto
            {
                Id = assignment.Id,
                Title = assignment.Title,
                Description = assignment.Description,
                CourseTitle = "Sample Course",
                DueDate = assignment.DueDate,
                MaxPoints = assignment.MaxPoints,
                Instructions = assignment.Instructions,
                AttachmentPath = assignment.AttachmentPath,
                IsActive = assignment.IsActive,
                CreatedAt = assignment.CreatedAt
            };
        }

        public async Task UpdateAssignmentAsync(int assignmentId, AssignmentDto assignmentDto)
        {
            var assignment = _assignments.FirstOrDefault(a => a.Id == assignmentId);
            if (assignment != null)
            {
                assignment.Title = assignmentDto.Title;
                assignment.Description = assignmentDto.Description;
                assignment.DueDate = assignmentDto.DueDate;
                assignment.MaxPoints = assignmentDto.MaxPoints;
                assignment.Instructions = assignmentDto.Instructions;
                assignment.AttachmentPath = assignmentDto.AttachmentPath;
            }
        }

        public async Task DeleteAssignmentAsync(int assignmentId)
        {
            var assignment = _assignments.FirstOrDefault(a => a.Id == assignmentId);
            if (assignment != null)
            {
                assignment.IsActive = false;
            }
        }

        public async Task<AssignmentResponseDto> GradeAssignmentAsync(int assignmentId, int studentId, int points, string feedback)
        {
            // In a real implementation, this would update the StudentAssignment record
            return new AssignmentResponseDto
            {
                Id = assignmentId,
                Title = "Sample Assignment",
                Description = "Assignment description",
                CourseTitle = "Sample Course",
                DueDate = DateTime.UtcNow.AddDays(7),
                MaxPoints = 100,
                Instructions = "Complete the assignment",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }
        #endregion

        #region Enrollment Management
        public async Task<IEnumerable<StudentCourseDto>> GetCourseEnrollmentsAsync(int courseId)
        {
            return new List<StudentCourseDto>
            {
                new StudentCourseDto
                {
                    CourseId = courseId,
                    CourseTitle = "Sample Course",
                    CourseCategory = "Technical",
                    EnrollmentDate = DateTime.UtcNow,
                    IsCompleted = false,
                    ProgressPercentage = 0,
                    PaymentStatus = "Completed",
                    AmountPaid = 99.99m
                }
            };
        }

        public async Task<bool> IsStudentEnrolledAsync(int studentId, int courseId)
        {
            return true; // Simplified for demo
        }

        public async Task<double> GetCourseProgressAsync(int studentId, int courseId)
        {
            return 75.5; // Simplified for demo
        }

        public async Task MarkCourseCompletedAsync(int studentId, int courseId)
        {
            // Implementation would mark course as completed
        }
        #endregion

        #region Content Management
        public async Task<Models.Course> UpdateCourseContentAsync(int courseId, string videoPath, string pdfPath)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course != null)
            {
                course.VideoPath = videoPath;
                course.PDFMaterialPath = pdfPath;
            }
            return course;
        }

        public async Task<string> GetCourseContentUrlAsync(int courseId, string contentType)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null) return null;

            return contentType.ToLower() switch
            {
                "video" => course.VideoPath,
                "pdf" => course.PDFMaterialPath,
                _ => null
            };
        }
        #endregion

        #region Analytics
        public async Task<CourseAnalyticsDto> GetCourseAnalyticsAsync(int courseId)
        {
            return new CourseAnalyticsDto
            {
                CourseId = courseId,
                CourseTitle = "Sample Course",
                TotalEnrollments = 50,
                ActiveStudents = 45,
                CompletedStudents = 30,
                CompletionRate = 60.0,
                AverageProgress = 75.5,
                AverageAssignmentScore = 85.2,
                WeeklyProgress = new List<WeeklyProgressDto>
                {
                    new WeeklyProgressDto { Week = DateTime.UtcNow.AddDays(-7), NewEnrollments = 5, Completions = 3, AverageProgress = 70.0 },
                    new WeeklyProgressDto { Week = DateTime.UtcNow, NewEnrollments = 8, Completions = 5, AverageProgress = 75.5 }
                }
            };
        }
        #endregion

        private void InitializeSampleData()
        {
            _courses.Add(new Models.Course
            {
                Id = 1,
                Title = "Programming Fundamentals",
                Description = "Learn basic programming concepts with C#",
                InstitutionId = 1,
                Category = "Technical",
                Price = 99.99m,
                DurationHours = 40,
                ThumbnailImagePath = "/images/course1.jpg",
                VideoPath = "/videos/course1.mp4",
                PDFMaterialPath = "/pdfs/course1.pdf",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(3)
            });

            _assignments.Add(new Assignment
            {
                Id = 1,
                Title = "Basic Programming Assignment",
                Description = "Write a simple C# program",
                CourseId = 1,
                DueDate = DateTime.UtcNow.AddDays(7),
                MaxPoints = 100,
                Instructions = "Create a console application that demonstrates basic programming concepts",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}