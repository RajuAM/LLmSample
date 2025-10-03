using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementLMS.DTOs.Placement;
using PlacementLMS.Services.Placement;

namespace PlacementLMS.Controllers
{
    [ApiController]
    [Route("api/company")]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        #region Company Management
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCompany([FromBody] CompanyRegistrationDto registrationDto)
        {
            try
            {
                var company = await _companyService.RegisterCompanyAsync(registrationDto);
                return CreatedAtAction(nameof(GetCompanyById), new { companyId = company.Id }, company);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{companyId}")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> GetCompanyById(int companyId)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);
            if (company == null)
                return NotFound(new { Message = "Company not found" });

            return Ok(company);
        }

        [HttpPut("{companyId}")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> UpdateCompany(int companyId, [FromBody] CompanyUpdateDto updateDto)
        {
            try
            {
                await _companyService.UpdateCompanyAsync(companyId, updateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("profile")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetCompanyProfile()
        {
            try
            {
                var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
                if (companyId == 0)
                    return Unauthorized(new { Message = "Company profile not found" });

                var company = await _companyService.GetCompanyByIdAsync(companyId);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Job Opportunity Management
        [HttpPost("jobs")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> CreateJobOpportunity([FromBody] JobOpportunityDto jobDto)
        {
            try
            {
                var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
                if (companyId == 0)
                    return Unauthorized(new { Message = "Company profile not found" });

                var job = await _companyService.CreateJobOpportunityAsync(companyId, jobDto);
                return CreatedAtAction(nameof(GetJobOpportunityById), new { jobId = job.Id }, job);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("jobs")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> GetCompanyJobOpportunities()
        {
            try
            {
                var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
                if (companyId == 0)
                    return Unauthorized(new { Message = "Company profile not found" });

                var jobs = await _companyService.GetCompanyJobOpportunitiesAsync(companyId);
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("jobs/{jobId}")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> GetJobOpportunityById(int jobId)
        {
            var job = await _companyService.GetJobOpportunityByIdAsync(jobId);
            if (job == null)
                return NotFound(new { Message = "Job opportunity not found" });

            return Ok(job);
        }

        [HttpPut("jobs/{jobId}")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> UpdateJobOpportunity(int jobId, [FromBody] JobOpportunityDto jobDto)
        {
            try
            {
                await _companyService.UpdateJobOpportunityAsync(jobId, jobDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("jobs/{jobId}")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> DeleteJobOpportunity(int jobId)
        {
            try
            {
                await _companyService.DeleteJobOpportunityAsync(jobId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("jobs/{jobId}/applications")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> GetJobApplications(int jobId)
        {
            var applications = await _companyService.GetJobApplicationsAsync(jobId);
            return Ok(applications);
        }
        #endregion

        #region Application Management
        [HttpPut("jobs/{jobId}/applications/{applicationId}/status")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> UpdateApplicationStatus(int jobId, int applicationId, [FromBody] ApplicationStatusDto statusDto)
        {
            try
            {
                await _companyService.UpdateApplicationStatusAsync(applicationId, statusDto.Status, statusDto.Feedback);
                return Ok(new { Message = "Application status updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("jobs/{jobId}/applications/{applicationId}/schedule-interview")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> ScheduleInterview(int jobId, int applicationId, [FromBody] InterviewScheduleDto scheduleDto)
        {
            try
            {
                await _companyService.ScheduleInterviewAsync(applicationId, scheduleDto.InterviewDate, scheduleDto.Notes);
                return Ok(new { Message = "Interview scheduled successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("applications/pending")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> GetPendingApplications()
        {
            try
            {
                var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
                if (companyId == 0)
                    return Unauthorized(new { Message = "Company profile not found" });

                var applications = await _companyService.GetPendingApplicationsAsync(companyId);
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("applications/shortlisted")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> GetShortlistedApplications()
        {
            try
            {
                var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
                if (companyId == 0)
                    return Unauthorized(new { Message = "Company profile not found" });

                var applications = await _companyService.GetShortlistedApplicationsAsync(companyId);
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Analytics and Reports
        [HttpGet("analytics")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> GetCompanyAnalytics()
        {
            try
            {
                var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
                if (companyId == 0)
                    return Unauthorized(new { Message = "Company profile not found" });

                var analytics = await _companyService.GetCompanyAnalyticsAsync(companyId);
                return Ok(analytics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("jobs/{jobId}/analytics")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> GetJobAnalytics(int jobId)
        {
            var analytics = await _companyService.GetJobAnalyticsAsync(jobId);
            return Ok(analytics);
        }
        #endregion
    }
}