using Microsoft.EntityFrameworkCore;
using PlacementLMS.Models;
using FeedbackModel = PlacementLMS.Models.Feedback;
using AssessmentModel = PlacementLMS.Models.Assessment;

namespace PlacementLMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for all models
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<JobOpportunity> JobOpportunities { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<MockTest> MockTests { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }
        public DbSet<StudentMockTest> StudentMockTests { get; set; }
        public DbSet<StudentTestAnswer> StudentTestAnswers { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<SessionRegistration> SessionRegistrations { get; set; }

        // Feedback and Assessment models
        public DbSet<FeedbackModel> Feedbacks { get; set; }
        public DbSet<AssessmentModel> Assessments { get; set; }
        public DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }
        public DbSet<StudentAssessment> StudentAssessments { get; set; }
        public DbSet<StudentAssessmentAnswer> StudentAssessmentAnswers { get; set; }

        // Analytics models (computed data, not stored in DB)
        // DashboardAnalytics is used for computed dashboard data

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentId)
                .IsUnique();

            // Feedback relationships
            modelBuilder.Entity<FeedbackModel>()
                .HasOne(f => f.FromUser)
                .WithMany(u => u.GivenFeedback)
                .HasForeignKey(f => f.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FeedbackModel>()
                .HasOne(f => f.ToUser)
                .WithMany(u => u.ReceivedFeedback)
                .HasForeignKey(f => f.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FeedbackModel>()
                .HasOne(f => f.Student)
                .WithMany(s => s.ReceivedFeedback)
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FeedbackModel>()
                .HasOne(f => f.Company)
                .WithMany(c => c.ReceivedFeedback)
                .HasForeignKey(f => f.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FeedbackModel>()
                .HasOne(f => f.Course)
                .WithMany(c => c.CourseFeedback)
                .HasForeignKey(f => f.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Assessment relationships
            modelBuilder.Entity<AssessmentModel>()
                .HasOne(a => a.CreatedBy)
                .WithMany(u => u.CreatedAssessments)
                .HasForeignKey(a => a.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssessmentModel>()
                .HasMany(a => a.Questions)
                .WithOne(q => q.Assessment)
                .HasForeignKey(q => q.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AssessmentModel>()
                .HasMany(a => a.StudentAssessments)
                .WithOne(sa => sa.Assessment)
                .HasForeignKey(sa => sa.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentAssessment>()
                .HasOne(sa => sa.Student)
                .WithMany(s => s.Assessments)
                .HasForeignKey(sa => sa.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentAssessment>()
                .HasMany(sa => sa.Answers)
                .WithOne(a => a.StudentAssessment)
                .HasForeignKey(a => a.StudentAssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AssessmentQuestion>()
                .HasMany(q => q.StudentAnswers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}