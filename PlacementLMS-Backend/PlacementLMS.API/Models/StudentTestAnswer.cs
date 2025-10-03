using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class StudentTestAnswer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("StudentMockTest")]
        public int StudentMockTestId { get; set; }

        [Required]
        [ForeignKey("TestQuestion")]
        public int TestQuestionId { get; set; }

        [Required]
        public string Answer { get; set; }

        public bool IsCorrect { get; set; } = false;

        public int MarksObtained { get; set; }

        // Navigation properties
        public virtual StudentMockTest StudentMockTest { get; set; }
        public virtual TestQuestion TestQuestion { get; set; }
    }
}