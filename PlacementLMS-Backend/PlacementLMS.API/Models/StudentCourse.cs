using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class StudentCourse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletionDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public double ProgressPercentage { get; set; } = 0;

        public decimal AmountPaid { get; set; }

        [StringLength(50)]
        public string PaymentStatus { get; set; } // Pending, Completed, Failed, Refunded

        [StringLength(100)]
        public string PaymentTransactionId { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}