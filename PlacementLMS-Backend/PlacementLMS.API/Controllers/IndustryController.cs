using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementLMS.DTOs.Placement;
using PlacementLMS.Services.Industry;

namespace PlacementLMS.Controllers
{
    [ApiController]
    [Route("api/industry")]
    [Authorize]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryService _industryService;

        public IndustryController(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        #region Company Management
        [HttpPost("register")]
        public async Task<IActionResult> RegisterCompany([FromBody] CompanyRegistrationDto companyDto)
        {
            try
            {
                var industry = await _industryService.RegisterCompanyAsync(companyDto);
                return Ok(new { Message = "Company registered successfully", IndustryId = industry.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetCompanyProfile()
        {
            try
            {
                var industryId = int.Parse(User.FindFirst("IndustryId")?.Value ?? "1");
                var profile = await _industryService.GetCompanyProfileAsync(industryId);
                if (profile == null)
                    return NotFound(new { Message = "Company profile not found" });

                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateCompanyProfile([FromBody] CompanyUpdateDto companyDto)
        {
            try
            {
                var industryId = int.Parse(User.FindFirst("IndustryId")?.Value ?? "1");
                var profile = await _industryService.UpdateCompanyProfileAsync(industryId, companyDto);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Job Management
        [HttpGet("jobs")]
        public async Task<IActionResult> GetAllJobOpportunities()
        {
            var jobs = await _industryService.GetAllJobOpportunitiesAsync();
            return Ok(jobs);
        }

        [HttpGet("jobs/{jobId}")]
        public async Task<IActionResult> GetJobOpportunityById(int jobId)
        {
            var job = await _industryService.GetJobOpportunityByIdAsync(jobId);
            if (job == null)
                return NotFound(new { Message = "Job opportunity not found" });

            return Ok(job);
        }

        [HttpPost("jobs")]
        public async Task<IActionResult> CreateJobOpportunity([FromBody] JobOpportunityDto jobDto)
        {
            try
            {
                var job = await _industryService.CreateJobOpportunityAsync(jobDto);
                return CreatedAtAction(nameof(GetJobOpportunityById), new { jobId = job.Id }, job);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("jobs/{jobId}")]
        public async Task<IActionResult> UpdateJobOpportunity(int jobId, [FromBody] JobOpportunityDto jobDto)
        {
            try
            {
                await _industryService.UpdateJobOpportunityAsync(jobId, jobDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("jobs/{jobId}")]
        public async Task<IActionResult> DeleteJobOpportunity(int jobId)
        {
            try
            {
                await _industryService.DeleteJobOpportunityAsync(jobId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("my-jobs")]
        public async Task<IActionResult> GetMyJobOpportunities()
        {
            var industryId = int.Parse(User.FindFirst("IndustryId")?.Value ?? "1");
            var jobs = await _industryService.GetJobsByIndustryAsync(industryId);
            return Ok(jobs);
        }

        [HttpGet("jobs/skills/{skills}")]
        public async Task<IActionResult> GetJobsBySkills(string skills)
        {
            var jobs = await _industryService.GetJobsBySkillsAsync(skills);
            return Ok(jobs);
        }
        #endregion

        #region Application Management
        [HttpGet("jobs/{jobId}/applications")]
        public async Task<IActionResult> GetJobApplications(int jobId)
        {
            var applications = await _industryService.GetJobApplicationsAsync(jobId);
            return Ok(applications);
        }

        [HttpGet("applications/{applicationId}")]
        public async Task<IActionResult> GetApplicationById(int applicationId)
        {
            var application = await _industryService.GetApplicationByIdAsync(applicationId);
            if (application == null)
                return NotFound(new { Message = "Application not found" });

            return Ok(application);
        }

        [HttpPut("applications/{applicationId}/status")]
        public async Task<IActionResult> UpdateApplicationStatus(int applicationId, [FromBody] UpdateApplicationStatusDto statusDto)
        {
            try
            {
                await _industryService.UpdateApplicationStatusAsync(applicationId, statusDto.Status, statusDto.Feedback);
                return Ok(new { Message = "Application status updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("interviews/schedule")]
        public async Task<IActionResult> ScheduleInterview([FromBody] InterviewScheduleDto scheduleDto)
        {
            try
            {
                await _industryService.ScheduleInterviewAsync(scheduleDto);
                return Ok(new { Message = "Interview scheduled successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("interviews/feedback")]
        public async Task<IActionResult> ProvideInterviewFeedback([FromBody] InterviewFeedbackDto feedbackDto)
        {
            try
            {
                await _industryService.ProvideInterviewFeedbackAsync(feedbackDto);
                return Ok(new { Message = "Interview feedback submitted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Analytics
        [HttpGet("analytics")]
        public async Task<IActionResult> GetIndustryAnalytics()
        {
            try
            {
                var industryId = int.Parse(User.FindFirst("IndustryId")?.Value ?? "1");
                var analytics = await _industryService.GetIndustryAnalyticsAsync(industryId);
                return Ok(analytics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("top-candidates")]
        public async Task<IActionResult> GetTopCandidates()
        {
            var industryId = int.Parse(User.FindFirst("IndustryId")?.Value ?? "1");
            var candidates = await _industryService.GetTopCandidatesAsync(industryId);
            return Ok(candidates);
        }
        #endregion
    }

    public class UpdateApplicationStatusDto
    {
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public string Feedback { get; set; }
    }
}