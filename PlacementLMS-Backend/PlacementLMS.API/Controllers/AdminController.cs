using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementLMS.DTOs.Admin;
using PlacementLMS.Models;
using PlacementLMS.Services.Admin;

namespace PlacementLMS.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize] // Require authentication for all admin endpoints
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #region Role Management Endpoints
        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto roleDto)
        {
            try
            {
                var role = await _adminService.CreateRoleAsync(roleDto);
                return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _adminService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("roles/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _adminService.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound(new { Message = "Role not found" });

            return Ok(role);
        }

        [HttpPut("roles/{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto roleDto)
        {
            try
            {
                await _adminService.UpdateRoleAsync(id, roleDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("roles/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                await _adminService.DeleteRoleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Department Management Endpoints
        [HttpPost("departments")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                var department = await _adminService.CreateDepartmentAsync(departmentDto);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _adminService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("departments/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _adminService.GetDepartmentByIdAsync(id);
            if (department == null)
                return NotFound(new { Message = "Department not found" });

            return Ok(department);
        }

        [HttpPut("departments/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            try
            {
                await _adminService.UpdateDepartmentAsync(id, departmentDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("departments/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                await _adminService.DeleteDepartmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region User Management Endpoints
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] UserManagementDto userDto)
        {
            try
            {
                var user = await _adminService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(user);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserManagementDto userDto)
        {
            try
            {
                await _adminService.UpdateUserAsync(id, userDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _adminService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("users/assign-roles")]
        public async Task<IActionResult> AssignUserRoles([FromBody] UserRoleAssignmentDto assignmentDto)
        {
            try
            {
                await _adminService.AssignUserRolesAsync(assignmentDto);
                return Ok(new { Message = "User roles assigned successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("users/type/{userType}")]
        public async Task<IActionResult> GetUsersByType(string userType)
        {
            var users = await _adminService.GetUsersByTypeAsync(userType);
            return Ok(users);
        }
        #endregion

        #region Permission Management Endpoints
        [HttpPost("permissions")]
        public async Task<IActionResult> CreatePermission([FromBody] PermissionDto permissionDto)
        {
            try
            {
                var permission = await _adminService.CreatePermissionAsync(permissionDto);
                return CreatedAtAction(nameof(GetPermissionById), new { id = permission.Id }, permission);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _adminService.GetAllPermissionsAsync();
            return Ok(permissions);
        }

        [HttpGet("permissions/{id}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var permission = await _adminService.GetPermissionByIdAsync(id);
            if (permission == null)
                return NotFound(new { Message = "Permission not found" });

            return Ok(permission);
        }

        [HttpPost("roles/assign-permissions")]
        public async Task<IActionResult> AssignRolePermissions([FromBody] RolePermissionAssignmentDto assignmentDto)
        {
            try
            {
                await _adminService.AssignRolePermissionsAsync(assignmentDto);
                return Ok(new { Message = "Role permissions assigned successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        #region Course Group Management Endpoints
        [HttpPost("course-groups")]
        public async Task<IActionResult> CreateCourseGroup([FromBody] CourseGroupDto courseGroupDto)
        {
            try
            {
                var courseGroup = await _adminService.CreateCourseGroupAsync(courseGroupDto);
                return CreatedAtAction(nameof(GetCourseGroupById), new { id = courseGroup.Id }, courseGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("course-groups")]
        public async Task<IActionResult> GetAllCourseGroups()
        {
            var courseGroups = await _adminService.GetAllCourseGroupsAsync();
            return Ok(courseGroups);
        }

        [HttpGet("course-groups/{id}")]
        public async Task<IActionResult> GetCourseGroupById(int id)
        {
            var courseGroup = await _adminService.GetCourseGroupByIdAsync(id);
            if (courseGroup == null)
                return NotFound(new { Message = "Course group not found" });

            return Ok(courseGroup);
        }

        [HttpPut("course-groups/{id}")]
        public async Task<IActionResult> UpdateCourseGroup(int id, [FromBody] CourseGroupDto courseGroupDto)
        {
            try
            {
                await _adminService.UpdateCourseGroupAsync(id, courseGroupDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("course-groups/{id}")]
        public async Task<IActionResult> DeleteCourseGroup(int id)
        {
            try
            {
                await _adminService.DeleteCourseGroupAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion
    }
}