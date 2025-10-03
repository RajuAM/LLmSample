using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Student
{
    public class StudentRegistrationDto
    {
        [Required]
        [StringLength(20)]
        public string StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string UniversityName { get; set; }

        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        [Required]
        public int CurrentSemester { get; set; }

        [Required]
        [Range(0.0, 10.0)]
        public double CGPA { get; set; }

        [Required]
        public DateTime GraduationDate { get; set; }

        public string ResumeFilePath { get; set; }
    }

    public class StudentProfileDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [StringLength(20)]
        public string StudentId { get; set; }

        [StringLength(100)]
        public string UniversityName { get; set; }

        [StringLength(100)]
        public string Department { get; set; }

        public int CurrentSemester { get; set; }

        [Range(0.0, 10.0)]
        public double CGPA { get; set; }

        public DateTime GraduationDate { get; set; }

        public string ResumeFilePath { get; set; }
    }

    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StudentId { get; set; }
        public string UniversityName { get; set; }
        public string Department { get; set; }
        public int CurrentSemester { get; set; }
        public double CGPA { get; set; }
        public string ResumeFilePath { get; set; }
        public bool IsProfileComplete { get; set; }
        public bool IsVerified { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime GraduationDate { get; set; }
        public List<StudentCourseDto> EnrolledCourses { get; set; } = new List<StudentCourseDto>();
        public List<CertificateDto> Certificates { get; set; } = new List<CertificateDto>();
    }

    public class StudentCourseDto
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCategory { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsCompleted { get; set; }
        public double ProgressPercentage { get; set; }
        public string PaymentStatus { get; set; }
        public decimal AmountPaid { get; set; }
    }

    public class CertificateDto
    {
        public int Id { get; set; }
        public string CertificateNumber { get; set; }
        public string CourseName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CertificateFilePath { get; set; }
    }
}