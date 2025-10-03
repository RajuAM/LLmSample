using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementLMS.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string StudentId { get; set; } // University student ID

        [Required]
        [StringLength(100)]
        public string UniversityName { get; set; }

        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        [Required]
        public int CurrentSemester { get; set; }

        [Required]
        public double CGPA { get; set; }

        public string ResumeFilePath { get; set; }

        public bool IsProfileComplete { get; set; } = false;

        public bool IsVerified { get; set; } = false;

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public DateTime GraduationDate { get; set; }

        // Navigation property
        public virtual User User { get; set; }

        // Collections
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentAssignment> StudentAssignments { get; set; }
        public virtual ICollection<StudentMockTest> StudentMockTests { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Feedback> ReceivedFeedback { get; set; }
        public virtual ICollection<StudentAssessment> Assessments { get; set; }
    }
}