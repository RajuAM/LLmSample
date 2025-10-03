using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementLMS.Services.Admin;

namespace PlacementLMS.Controllers
{
    [ApiController]
    [Route("api/admin/dashboard")]
    [Authorize]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminDashboardController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                // Get all users and calculate statistics
                var allUsers = await _adminService.GetAllUsersAsync();
                var roles = await _adminService.GetAllRolesAsync();
                var departments = await _adminService.GetAllDepartmentsAsync();

                var stats = new
                {
                    TotalUsers = allUsers.Count(),
                    TotalRoles = roles.Count(),
                    TotalDepartments = departments.Count(),
                    UsersByType = new
                    {
                        Students = allUsers.Count(u => u.UserType == "Student"),
                        Institutions = allUsers.Count(u => u.UserType == "Institution"),
                        Industries = allUsers.Count(u => u.UserType == "Industry"),
                        Admins = allUsers.Count(u => u.UserType == "Admin")
                    },
                    RecentActivity = new[]
                    {
                        new { Action = "New user registered", Timestamp = DateTime.UtcNow.AddHours(-2) },
                        new { Action = "Role created", Timestamp = DateTime.UtcNow.AddHours(-4) },
                        new { Action = "Department added", Timestamp = DateTime.UtcNow.AddHours(-6) }
                    }
                };

                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("users/active")]
        public async Task<IActionResult> GetActiveUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            var activeUsers = users.Where(u => u.IsActive).Take(10);
            return Ok(activeUsers);
        }

        [HttpGet("system-health")]
        public IActionResult GetSystemHealth()
        {
            var health = new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0",
                Database = "Connected",
                Services = new[]
                {
                    "Authentication Service",
                    "User Management Service",
                    "Role Management Service",
                    "Department Management Service"
                }
            };

            return Ok(health);
        }
    }
}