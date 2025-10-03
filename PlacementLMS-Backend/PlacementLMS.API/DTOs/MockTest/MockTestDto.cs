using System.ComponentModel.DataAnnotations;

namespace PlacementLMS.DTOs.MockTest
{
    public class MockTestDto
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
        public string Subject { get; set; }

        [Required]
        [Range(1, 300)]
        public int DurationMinutes { get; set; }

        [Required]
        [Range(1, 200)]
        public int TotalQuestions { get; set; }

        [Required]
        [Range(1, 1000)]
        public int MaxMarks { get; set; }

        public string Instructions { get; set; }

        public DateTime ScheduledDate { get; set; }
    }

    public class MockTestResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InstitutionName { get; set; }
        public string Subject { get; set; }
        public int DurationMinutes { get; set; }
        public int TotalQuestions { get; set; }
        public int MaxMarks { get; set; }
        public string Instructions { get; set; }
        public bool IsActive { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ParticipantCount { get; set; }
        public List<TestQuestionDto> Questions { get; set; } = new List<TestQuestionDto>();
    }

    public class TestQuestionDto
    {
        [Required]
        public int MockTestId { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionType { get; set; }

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
        [Range(1, 100)]
        public int Marks { get; set; }

        public int QuestionOrder { get; set; }
    }

    public class StudentMockTestDto
    {
        [Required]
        public int MockTestId { get; set; }

        [Required]
        public List<StudentTestAnswerDto> Answers { get; set; } = new List<StudentTestAnswerDto>();
    }

    public class StudentTestAnswerDto
    {
        [Required]
        public int TestQuestionId { get; set; }

        [Required]
        public string Answer { get; set; }
    }

    public class StudentMockTestResponseDto
    {
        public int Id { get; set; }
        public string MockTestTitle { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public double Percentage { get; set; }
        public string Status { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public List<StudentTestAnswerDto> Answers { get; set; } = new List<StudentTestAnswerDto>();
    }

    public class TestResultDto
    {
        public int MockTestId { get; set; }
        public string MockTestTitle { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public double Percentage { get; set; }
        public string Grade { get; set; }
        public DateTime CompletedAt { get; set; }
        public List<QuestionResultDto> QuestionResults { get; set; } = new List<QuestionResultDto>();
    }

    public class QuestionResultDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }
        public string StudentAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int MarksObtained { get; set; }
    }
}