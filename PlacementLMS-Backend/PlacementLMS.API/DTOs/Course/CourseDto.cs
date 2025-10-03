using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.Course
{
    public class CourseDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int InstitutionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public decimal DiscountPrice { get; set; }

        [Required]
        [Range(1, 1000)]
        public int DurationHours { get; set; }

        public string ThumbnailImagePath { get; set; }

        public string VideoPath { get; set; }

        public string PDFMaterialPath { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class CourseResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InstitutionName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int DurationHours { get; set; }
        public string ThumbnailImagePath { get; set; }
        public string VideoPath { get; set; }
        public string PDFMaterialPath { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EnrollmentCount { get; set; }
        public List<AssignmentDto> Assignments { get; set; } = new List<AssignmentDto>();
    }

    public class CourseEnrollmentDto
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal AmountPaid { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; }

        [StringLength(100)]
        public string PaymentTransactionId { get; set; }
    }

    public class AssignmentDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [Range(0, 1000)]
        public int MaxPoints { get; set; }

        public string Instructions { get; set; }

        public string AttachmentPath { get; set; }
    }

    public class AssignmentResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseTitle { get; set; }
        public DateTime DueDate { get; set; }
        public int MaxPoints { get; set; }
        public string Instructions { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<StudentAssignmentDto> Submissions { get; set; } = new List<StudentAssignmentDto>();
    }

    public class StudentAssignmentDto
    {
        [Required]
        public int AssignmentId { get; set; }

        public string SubmissionText { get; set; }

        public string SubmissionFilePath { get; set; }
    }

    public class StudentAssignmentResponseDto
    {
        public int Id { get; set; }
        public string AssignmentTitle { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string SubmissionFilePath { get; set; }
        public string SubmissionText { get; set; }
        public int? PointsScored { get; set; }
        public string Feedback { get; set; }
        public string Status { get; set; }
        public DateTime? GradedAt { get; set; }
    }
}