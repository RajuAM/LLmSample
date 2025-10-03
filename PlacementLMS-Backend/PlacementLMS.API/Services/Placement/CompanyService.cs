using PlacementLMS.DTOs.Placement;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Placement
{
    public class CompanyService : ICompanyService
    {
        private readonly List<Company> _companies = new List<Company>();
        private readonly List<JobOpportunity> _jobOpportunities = new List<JobOpportunity>();
        private readonly List<JobApplication> _jobApplications = new List<JobApplication>();
        private int _nextCompanyId = 1;
        private int _nextJobId = 1;
        private int _nextApplicationId = 1;

        public CompanyService()
        {
            InitializeSampleData();
        }

        public async Task<Company> RegisterCompanyAsync(CompanyRegistrationDto registrationDto)
        {
            // Check if company with same email already exists
            var existingCompany = _companies.FirstOrDefault(c => c.ContactEmail == registrationDto.ContactEmail);

            if (existingCompany != null)
                throw new Exception("Company with this email already exists");

            var company = new Company
            {
                Id = _nextCompanyId++,
                Name = registrationDto.Name,
                Industry = registrationDto.Industry,
                Description = registrationDto.Description,
                Website = registrationDto.Website,
                CompanySize = registrationDto.CompanySize,
                Address = registrationDto.Address,
                City = registrationDto.City,
                State = registrationDto.State,
                PostalCode = registrationDto.PostalCode,
                Country = registrationDto.Country,
                ContactNumber = registrationDto.ContactNumber,
                ContactEmail = registrationDto.ContactEmail,
                HRContactName = registrationDto.HRContactName,
                HRContactNumber = registrationDto.HRContactNumber,
                HRContactEmail = registrationDto.HRContactEmail,
                IsVerified = false,
                IsActive = true,
                RegistrationDate = DateTime.UtcNow
            };

            _companies.Add(company);
            return company;
        }

        public async Task<Company> GetCompanyByIdAsync(int companyId)
        {
            return _companies.FirstOrDefault(c => c.Id == companyId && c.IsActive);
        }

        public async Task UpdateCompanyAsync(int companyId, CompanyUpdateDto updateDto)
        {
            var company = _companies.FirstOrDefault(c => c.Id == companyId);
            if (company == null)
                throw new Exception("Company not found");

            // Update only provided fields
            if (!string.IsNullOrEmpty(updateDto.Name))
                company.Name = updateDto.Name;
            if (!string.IsNullOrEmpty(updateDto.Description))
                company.Description = updateDto.Description;
            if (!string.IsNullOrEmpty(updateDto.Website))
                company.Website = updateDto.Website;
            if (!string.IsNullOrEmpty(updateDto.CompanySize))
                company.CompanySize = updateDto.CompanySize;
            if (!string.IsNullOrEmpty(updateDto.Address))
                company.Address = updateDto.Address;
            if (!string.IsNullOrEmpty(updateDto.City))
                company.City = updateDto.City;
            if (!string.IsNullOrEmpty(updateDto.State))
                company.State = updateDto.State;
            if (!string.IsNullOrEmpty(updateDto.PostalCode))
                company.PostalCode = updateDto.PostalCode;
            if (!string.IsNullOrEmpty(updateDto.Country))
                company.Country = updateDto.Country;
            if (!string.IsNullOrEmpty(updateDto.ContactNumber))
                company.ContactNumber = updateDto.ContactNumber;
            if (!string.IsNullOrEmpty(updateDto.ContactEmail))
                company.ContactEmail = updateDto.ContactEmail;
            if (!string.IsNullOrEmpty(updateDto.HRContactName))
                company.HRContactName = updateDto.HRContactName;
            if (!string.IsNullOrEmpty(updateDto.HRContactNumber))
                company.HRContactNumber = updateDto.HRContactNumber;
            if (!string.IsNullOrEmpty(updateDto.HRContactEmail))
                company.HRContactEmail = updateDto.HRContactEmail;
        }

        public async Task<JobOpportunity> CreateJobOpportunityAsync(int companyId, JobOpportunityDto jobDto)
        {
            var company = _companies.FirstOrDefault(c => c.Id == companyId);
            if (company == null)
                throw new Exception("Company not found");

            var jobOpportunity = new JobOpportunity
            {
                Id = _nextJobId++,
                Title = jobDto.Title,
                Description = jobDto.Description,
                IndustryId = jobDto.IndustryId,
                CompanyId = companyId,
                JobType = jobDto.JobType,
                ExperienceLevel = jobDto.ExperienceLevel,
                Location = jobDto.Location,
                SalaryMin = jobDto.SalaryMin,
                SalaryMax = jobDto.SalaryMax,
                SkillsRequired = jobDto.SkillsRequired,
                Responsibilities = jobDto.Responsibilities,
                Benefits = jobDto.Benefits,
                NumberOfPositions = jobDto.NumberOfPositions,
                ApplicationDeadline = jobDto.ApplicationDeadline,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _jobOpportunities.Add(jobOpportunity);
            return jobOpportunity;
        }

        public async Task<IEnumerable<JobOpportunityResponseDto>> GetCompanyJobOpportunitiesAsync(int companyId)
        {
            return _jobOpportunities.Where(j => j.CompanyId == companyId && j.IsActive)
                .Select(j => new JobOpportunityResponseDto
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    CompanyName = _companies.FirstOrDefault(c => c.Id == j.CompanyId)?.Name ?? "Unknown",
                    JobType = j.JobType,
                    ExperienceLevel = j.ExperienceLevel,
                    Location = j.Location,
                    SalaryMin = j.SalaryMin,
                    SalaryMax = j.SalaryMax,
                    SkillsRequired = j.SkillsRequired,
                    Responsibilities = j.Responsibilities,
                    Benefits = j.Benefits,
                    NumberOfPositions = j.NumberOfPositions,
                    NumberOfApplications = j.NumberOfApplications,
                    ApplicationDeadline = j.ApplicationDeadline,
                    IsActive = j.IsActive,
                    CreatedAt = j.CreatedAt,
                    Applications = new List<JobApplicationDto>()
                });
        }

        public async Task<JobOpportunity> GetJobOpportunityByIdAsync(int jobId)
        {
            return _jobOpportunities.FirstOrDefault(j => j.Id == jobId && j.IsActive);
        }

        public async Task UpdateJobOpportunityAsync(int jobId, JobOpportunityDto jobDto)
        {
            var job = _jobOpportunities.FirstOrDefault(j => j.Id == jobId);
            if (job == null)
                throw new Exception("Job opportunity not found");

            job.Title = jobDto.Title;
            job.Description = jobDto.Description;
            job.IndustryId = jobDto.IndustryId;
            job.JobType = jobDto.JobType;
            job.ExperienceLevel = jobDto.ExperienceLevel;
            job.Location = jobDto.Location;
            job.SalaryMin = jobDto.SalaryMin;
            job.SalaryMax = jobDto.SalaryMax;
            job.SkillsRequired = jobDto.SkillsRequired;
            job.Responsibilities = jobDto.Responsibilities;
            job.Benefits = jobDto.Benefits;
            job.NumberOfPositions = jobDto.NumberOfPositions;
            job.ApplicationDeadline = jobDto.ApplicationDeadline;
        }

        public async Task DeleteJobOpportunityAsync(int jobId)
        {
            var job = _jobOpportunities.FirstOrDefault(j => j.Id == jobId);
            if (job == null)
                throw new Exception("Job opportunity not found");

            job.IsActive = false;
        }

        public async Task<IEnumerable<JobApplicationResponseDto>> GetJobApplicationsAsync(int jobId)
        {
            return _jobApplications.Where(a => a.JobOpportunityId == jobId)
                .Select(a => new JobApplicationResponseDto
                {
                    Id = a.Id,
                    JobTitle = _jobOpportunities.FirstOrDefault(j => j.Id == a.JobOpportunityId)?.Title ?? "Unknown",
                    CompanyName = _companies.FirstOrDefault(c => c.Id == _jobOpportunities.FirstOrDefault(j => j.Id == a.JobOpportunityId)?.CompanyId)?.Name ?? "Unknown",
                    StudentName = "Sample Student", // Would need actual student data
                    AppliedAt = a.AppliedAt,
                    Status = a.Status,
                    CoverLetter = a.CoverLetter,
                    InterviewDate = a.InterviewDate,
                    InterviewFeedback = a.InterviewFeedback,
                    IsSelected = a.IsSelected,
                    SelectionDate = a.SelectionDate
                });
        }

        public async Task UpdateApplicationStatusAsync(int applicationId, string status, string feedback)
        {
            var application = _jobApplications.FirstOrDefault(a => a.Id == applicationId);
            if (application == null)
                throw new Exception("Application not found");

            application.Status = status;
            application.InterviewFeedback = feedback;

            if (status == "Selected")
            {
                application.IsSelected = true;
                application.SelectionDate = DateTime.UtcNow;
            }
        }

        public async Task ScheduleInterviewAsync(int applicationId, DateTime interviewDate, string notes)
        {
            var application = _jobApplications.FirstOrDefault(a => a.Id == applicationId);
            if (application == null)
                throw new Exception("Application not found");

            application.InterviewDate = interviewDate;
            application.InterviewFeedback = notes;
            application.Status = "Interview Scheduled";
        }

        public async Task<IEnumerable<JobApplicationResponseDto>> GetPendingApplicationsAsync(int companyId)
        {
            return _jobApplications.Where(a =>
                _jobOpportunities.FirstOrDefault(j => j.Id == a.JobOpportunityId)?.CompanyId == companyId &&
                a.Status == "Applied")
                .Select(a => new JobApplicationResponseDto
                {
                    Id = a.Id,
                    JobTitle = _jobOpportunities.FirstOrDefault(j => j.Id == a.JobOpportunityId)?.Title ?? "Unknown",
                    CompanyName = _companies.FirstOrDefault(c => c.Id == companyId)?.Name ?? "Unknown",
                    StudentName = "Sample Student",
                    AppliedAt = a.AppliedAt,
                    Status = a.Status,
                    CoverLetter = a.CoverLetter,
                    InterviewDate = a.InterviewDate,
                    InterviewFeedback = a.InterviewFeedback,
                    IsSelected = a.IsSelected,
                    SelectionDate = a.SelectionDate
                });
        }

        public async Task<IEnumerable<JobApplicationResponseDto>> GetShortlistedApplicationsAsync(int companyId)
        {
            return _jobApplications.Where(a =>
                _jobOpportunities.FirstOrDefault(j => j.Id == a.JobOpportunityId)?.CompanyId == companyId &&
                a.Status == "Shortlisted")
                .Select(a => new JobApplicationResponseDto
                {
                    Id = a.Id,
                    JobTitle = _jobOpportunities.FirstOrDefault(j => j.Id == a.JobOpportunityId)?.Title ?? "Unknown",
                    CompanyName = _companies.FirstOrDefault(c => c.Id == companyId)?.Name ?? "Unknown",
                    StudentName = "Sample Student",
                    AppliedAt = a.AppliedAt,
                    Status = a.Status,
                    CoverLetter = a.CoverLetter,
                    InterviewDate = a.InterviewDate,
                    InterviewFeedback = a.InterviewFeedback,
                    IsSelected = a.IsSelected,
                    SelectionDate = a.SelectionDate
                });
        }

        public async Task<PlacementStatsDto> GetCompanyAnalyticsAsync(int companyId)
        {
            var companyJobs = _jobOpportunities.Where(j => j.CompanyId == companyId && j.IsActive);
            var companyApplications = _jobApplications.Where(a =>
                _jobOpportunities.FirstOrDefault(j => j.Id == a.JobOpportunityId)?.CompanyId == companyId);

            var totalApplications = companyApplications.Count();
            var totalInterviews = companyApplications.Count(a => a.InterviewDate != null);
            var totalSelections = companyApplications.Count(a => a.IsSelected);

            var placementRate = totalApplications > 0 ? (double)totalSelections / totalApplications * 100 : 0;

            return new PlacementStatsDto
            {
                TotalJobOpportunities = companyJobs.Count(),
                TotalApplications = totalApplications,
                TotalInterviews = totalInterviews,
                TotalSelections = totalSelections,
                PlacementRate = placementRate
            };
        }

        public async Task<JobOpportunityResponseDto> GetJobAnalyticsAsync(int jobId)
        {
            var job = _jobOpportunities.FirstOrDefault(j => j.Id == jobId && j.IsActive);
            if (job == null) return null;

            return new JobOpportunityResponseDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                CompanyName = _companies.FirstOrDefault(c => c.Id == job.CompanyId)?.Name ?? "Unknown",
                JobType = job.JobType,
                ExperienceLevel = job.ExperienceLevel,
                Location = job.Location,
                SalaryMin = job.SalaryMin,
                SalaryMax = job.SalaryMax,
                SkillsRequired = job.SkillsRequired,
                NumberOfPositions = job.NumberOfPositions,
                NumberOfApplications = job.NumberOfApplications,
                ApplicationDeadline = job.ApplicationDeadline,
                IsActive = job.IsActive,
                CreatedAt = job.CreatedAt
            };
        }

        private void InitializeSampleData()
        {
            // Add sample companies
            _companies.Add(new Company
            {
                Id = 1,
                Name = "Tech Solutions Inc.",
                Industry = "Information Technology",
                Description = "Leading IT solutions provider",
                Website = "https://techsolutions.com",
                CompanySize = "Large",
                Address = "123 Tech Street",
                City = "Mumbai",
                State = "Maharashtra",
                PostalCode = "400001",
                Country = "India",
                ContactNumber = "022-12345678",
                ContactEmail = "hr@techsolutions.com",
                HRContactName = "Priya Sharma",
                HRContactNumber = "022-12345679",
                HRContactEmail = "priya.sharma@techsolutions.com",
                IsVerified = true,
                IsActive = true,
                RegistrationDate = DateTime.UtcNow
            });

            _companies.Add(new Company
            {
                Id = 2,
                Name = "Data Systems Corp",
                Industry = "Data Analytics",
                Description = "Data-driven solutions company",
                Website = "https://datasystems.com",
                CompanySize = "Medium",
                Address = "456 Data Avenue",
                City = "Bangalore",
                State = "Karnataka",
                PostalCode = "560001",
                Country = "India",
                ContactNumber = "080-87654321",
                ContactEmail = "careers@datasystems.com",
                HRContactName = "Rajesh Kumar",
                HRContactNumber = "080-87654322",
                HRContactEmail = "rajesh.kumar@datasystems.com",
                IsVerified = true,
                IsActive = true,
                RegistrationDate = DateTime.UtcNow
            });

            // Add sample job opportunities
            _jobOpportunities.Add(new JobOpportunity
            {
                Id = 1,
                Title = "Software Developer",
                Description = "Full-stack software development position",
                IndustryId = 1,
                CompanyId = 1,
                JobType = "Full-time",
                ExperienceLevel = "Entry",
                Location = "Mumbai, Maharashtra",
                SalaryMin = 600000,
                SalaryMax = 900000,
                SkillsRequired = "C#, ASP.NET, SQL, JavaScript",
                Responsibilities = "Develop web applications, maintain code quality, collaborate with team",
                Benefits = "Health insurance, flexible work hours, professional development",
                NumberOfPositions = 5,
                NumberOfApplications = 0,
                ApplicationDeadline = DateTime.UtcNow.AddMonths(1),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });

            _jobOpportunities.Add(new JobOpportunity
            {
                Id = 2,
                Title = "Junior Developer",
                Description = "Entry-level software development position",
                IndustryId = 1,
                CompanyId = 2,
                JobType = "Full-time",
                ExperienceLevel = "Entry",
                Location = "Bangalore, Karnataka",
                SalaryMin = 400000,
                SalaryMax = 600000,
                SkillsRequired = "Python, SQL, Basic Programming",
                Responsibilities = "Assist in application development, learn new technologies",
                Benefits = "Training programs, health benefits, career growth",
                NumberOfPositions = 3,
                NumberOfApplications = 0,
                ApplicationDeadline = DateTime.UtcNow.AddMonths(2),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });

            // Add sample job applications
            _jobApplications.Add(new JobApplication
            {
                Id = 1,
                StudentId = 1,
                JobOpportunityId = 1,
                AppliedAt = DateTime.UtcNow,
                Status = "Applied",
                CoverLetter = "I am very interested in this position...",
                InterviewDate = null,
                InterviewFeedback = null,
                IsSelected = false,
                SelectionDate = null
            });
        }
    }
}