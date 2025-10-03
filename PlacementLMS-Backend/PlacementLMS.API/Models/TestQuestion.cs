using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class TestQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("MockTest")]
        public int MockTestId { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionType { get; set; } // Multiple Choice, True/False, Short Answer

        [Required]
        public string OptionA { get; set; }

        [Required]
        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public string OptionD { get; set; }

        [Required]
        [StringLength(10)]
        public string CorrectAnswer { get; set; }

        [Required]
        public int Marks { get; set; }

        public int QuestionOrder { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation property
        public virtual MockTest MockTest { get; set; }

        // Collections
        public virtual ICollection<StudentTestAnswer> StudentAnswers { get; set; }
    }
}