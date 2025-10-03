using PlacementLMS.DTOs.Admin;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Admin
{
    public interface IAdminService
    {
        // Role Management
        Task<Role> CreateRoleAsync(RoleDto roleDto);
        Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync();
        Task<RoleResponseDto> GetRoleByIdAsync(int id);
        Task UpdateRoleAsync(int id, RoleDto roleDto);
        Task DeleteRoleAsync(int id);

        // Department Management
        Task<Department> CreateDepartmentAsync(DepartmentDto departmentDto);
        Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentsAsync();
        Task<DepartmentResponseDto> GetDepartmentByIdAsync(int id);
        Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto);
        Task DeleteDepartmentAsync(int id);

        // User Management
        Task<User> CreateUserAsync(UserManagementDto userDto);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> GetUserByIdAsync(int id);
        Task UpdateUserAsync(int id, UserManagementDto userDto);
        Task DeleteUserAsync(int id);
        Task AssignUserRolesAsync(UserRoleAssignmentDto assignmentDto);
        Task<IEnumerable<UserResponseDto>> GetUsersByTypeAsync(string userType);

        // Permission Management
        Task<Permission> CreatePermissionAsync(PermissionDto permissionDto);
        Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync();
        Task<PermissionResponseDto> GetPermissionByIdAsync(int id);
        Task AssignRolePermissionsAsync(RolePermissionAssignmentDto assignmentDto);

        // Course Group Management
        Task<CourseGroup> CreateCourseGroupAsync(CourseGroupDto courseGroupDto);
        Task<IEnumerable<CourseGroupResponseDto>> GetAllCourseGroupsAsync();
        Task<CourseGroupResponseDto> GetCourseGroupByIdAsync(int id);
        Task UpdateCourseGroupAsync(int id, CourseGroupDto courseGroupDto);
        Task DeleteCourseGroupAsync(int id);
    }
}