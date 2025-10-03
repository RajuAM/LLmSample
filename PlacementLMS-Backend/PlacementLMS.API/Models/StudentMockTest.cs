using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class StudentMockTest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("MockTest")]
        public int MockTestId { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public int MaxScore { get; set; }

        public double Percentage { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "In Progress"; // In Progress, Completed, Absent

        public TimeSpan TimeTaken { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual MockTest MockTest { get; set; }

        // Collections
        public virtual ICollection<StudentTestAnswer> Answers { get; set; }
    }
}