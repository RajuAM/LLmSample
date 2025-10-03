using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class MockTest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Institution")]
        public int InstitutionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        [Required]
        public int TotalQuestions { get; set; }

        [Required]
        public int MaxMarks { get; set; }

        public string Instructions { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime ScheduledDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual Institution Institution { get; set; }

        // Collections
        public virtual ICollection<StudentMockTest> StudentMockTests { get; set; }
        public virtual ICollection<TestQuestion> Questions { get; set; }
    }
}