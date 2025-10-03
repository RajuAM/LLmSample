using System.Security.Cryptography;
using System.Text;
using PlacementLMS.DTOs.Admin;
using PlacementLMS.Models;

namespace PlacementLMS.Services.Admin
{
    public class AdminService : IAdminService
    {
        // In a real application, you would inject a database context here
        // For now, this is a basic implementation structure

        private readonly List<Role> _roles = new List<Role>();
        private readonly List<Department> _departments = new List<Department>();
        private readonly List<User> _users = new List<User>();
        private readonly List<Permission> _permissions = new List<Permission>();
        private readonly List<CourseGroup> _courseGroups = new List<CourseGroup>();
        private int _nextRoleId = 1;
        private int _nextDepartmentId = 1;
        private int _nextUserId = 1;
        private int _nextPermissionId = 1;
        private int _nextCourseGroupId = 1;

        public AdminService()
        {
            // Initialize with default roles and permissions
            InitializeDefaultRoles();
            InitializeDefaultPermissions();
        }

        #region Role Management
        public async Task<Role> CreateRoleAsync(RoleDto roleDto)
        {
            var role = new Role
            {
                Id = _nextRoleId++,
                Name = roleDto.Name,
                Description = roleDto.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _roles.Add(role);
            return role;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
        {
            return _roles.Where(r => r.IsActive).Select(r => new RoleResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IsActive = r.IsActive,
                CreatedAt = r.CreatedAt,
                UserCount = 0 // Would be calculated from database
            });
        }

        public async Task<RoleResponseDto> GetRoleByIdAsync(int id)
        {
            var role = _roles.FirstOrDefault(r => r.Id == id && r.IsActive);
            if (role == null) return null;

            return new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsActive = role.IsActive,
                CreatedAt = role.CreatedAt,
                UserCount = 0
            };
        }

        public async Task UpdateRoleAsync(int id, RoleDto roleDto)
        {
            var role = _roles.FirstOrDefault(r => r.Id == id);
            if (role != null)
            {
                role.Name = roleDto.Name;
                role.Description = roleDto.Description;
                role.UpdatedAt = DateTime.UtcNow;
            }
        }

        public async Task DeleteRoleAsync(int id)
        {
            var role = _roles.FirstOrDefault(r => r.Id == id);
            if (role != null)
            {
                role.IsActive = false;
            }
        }
        #endregion

        #region Department Management
        public async Task<Department> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                Id = _nextDepartmentId++,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                Code = departmentDto.Code,
                InstitutionId = departmentDto.InstitutionId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _departments.Add(department);
            return department;
        }

        public async Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentsAsync()
        {
            return _departments.Where(d => d.IsActive).Select(d => new DepartmentResponseDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Code = d.Code,
                InstitutionId = d.InstitutionId,
                InstitutionName = "Sample Institution", // Would be fetched from database
                IsActive = d.IsActive,
                CreatedAt = d.CreatedAt,
                StudentCount = 0
            });
        }

        public async Task<DepartmentResponseDto> GetDepartmentByIdAsync(int id)
        {
            var department = _departments.FirstOrDefault(d => d.Id == id && d.IsActive);
            if (department == null) return null;

            return new DepartmentResponseDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                Code = department.Code,
                InstitutionId = department.InstitutionId,
                InstitutionName = "Sample Institution",
                IsActive = department.IsActive,
                CreatedAt = department.CreatedAt,
                StudentCount = 0
            };
        }

        public async Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto)
        {
            var department = _departments.FirstOrDefault(d => d.Id == id);
            if (department != null)
            {
                department.Name = departmentDto.Name;
                department.Description = departmentDto.Description;
                department.Code = departmentDto.Code;
                department.InstitutionId = departmentDto.InstitutionId;
                department.UpdatedAt = DateTime.UtcNow;
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = _departments.FirstOrDefault(d => d.Id == id);
            if (department != null)
            {
                department.IsActive = false;
            }
        }
        #endregion

        #region User Management
        public async Task<User> CreateUserAsync(UserManagementDto userDto)
        {
            var user = new User
            {
                Id = _nextUserId++,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PasswordHash = HashPassword("DefaultPassword123"), // In real app, generate proper password
                PhoneNumber = userDto.PhoneNumber,
                DateOfBirth = userDto.DateOfBirth,
                Address = userDto.Address,
                UserType = userDto.UserType,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _users.Add(user);

            // Assign roles if provided
            if (userDto.RoleIds.Any())
            {
                await AssignUserRolesAsync(new UserRoleAssignmentDto
                {
                    UserId = user.Id,
                    RoleIds = userDto.RoleIds
                });
            }

            return user;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            return _users.Where(u => u.IsActive).Select(u => new UserResponseDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserType = u.UserType,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt,
                Roles = new List<string> { "Default Role" }, // Would be fetched from UserRole table
                DepartmentName = "Default Department",
                InstitutionName = "Default Institution"
            });
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id && u.IsActive);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Roles = new List<string> { "Default Role" },
                DepartmentName = "Default Department",
                InstitutionName = "Default Institution"
            };
        }

        public async Task UpdateUserAsync(int id, UserManagementDto userDto)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                user.PhoneNumber = userDto.PhoneNumber;
                user.DateOfBirth = userDto.DateOfBirth;
                user.Address = userDto.Address;
                user.UserType = userDto.UserType;
                user.UpdatedAt = DateTime.UtcNow;
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsActive = false;
            }
        }

        public async Task AssignUserRolesAsync(UserRoleAssignmentDto assignmentDto)
        {
            // Implementation would add/remove UserRole records
            // For now, this is a placeholder
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersByTypeAsync(string userType)
        {
            return _users.Where(u => u.IsActive && u.UserType == userType)
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    UserType = u.UserType,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    Roles = new List<string> { "Default Role" },
                    DepartmentName = "Default Department",
                    InstitutionName = "Default Institution"
                });
        }
        #endregion

        #region Permission Management
        public async Task<Permission> CreatePermissionAsync(PermissionDto permissionDto)
        {
            var permission = new Permission
            {
                Id = _nextPermissionId++,
                Name = permissionDto.Name,
                Description = permissionDto.Description,
                Module = permissionDto.Module,
                Action = permissionDto.Action,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _permissions.Add(permission);
            return permission;
        }

        public async Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync()
        {
            return _permissions.Where(p => p.IsActive).Select(p => new PermissionResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module,
                Action = p.Action,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                RoleCount = 0 // Would be calculated from RolePermission table
            });
        }

        public async Task<PermissionResponseDto> GetPermissionByIdAsync(int id)
        {
            var permission = _permissions.FirstOrDefault(p => p.Id == id && p.IsActive);
            if (permission == null) return null;

            return new PermissionResponseDto
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description,
                Module = permission.Module,
                Action = permission.Action,
                IsActive = permission.IsActive,
                CreatedAt = permission.CreatedAt,
                RoleCount = 0
            };
        }

        public async Task AssignRolePermissionsAsync(RolePermissionAssignmentDto assignmentDto)
        {
            // Implementation would add/remove RolePermission records
            // For now, this is a placeholder
        }
        #endregion

        #region Course Group Management
        public async Task<CourseGroup> CreateCourseGroupAsync(CourseGroupDto courseGroupDto)
        {
            var courseGroup = new CourseGroup
            {
                Id = _nextCourseGroupId++,
                Name = courseGroupDto.Name,
                Description = courseGroupDto.Description,
                DepartmentId = courseGroupDto.DepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _courseGroups.Add(courseGroup);
            return courseGroup;
        }

        public async Task<IEnumerable<CourseGroupResponseDto>> GetAllCourseGroupsAsync()
        {
            return _courseGroups.Where(cg => cg.IsActive).Select(cg => new CourseGroupResponseDto
            {
                Id = cg.Id,
                Name = cg.Name,
                Description = cg.Description,
                DepartmentId = cg.DepartmentId,
                DepartmentName = "Sample Department", // Would be fetched from Department table
                IsActive = cg.IsActive,
                CreatedAt = cg.CreatedAt,
                CourseCount = 0 // Would be calculated from Course table
            });
        }

        public async Task<CourseGroupResponseDto> GetCourseGroupByIdAsync(int id)
        {
            var courseGroup = _courseGroups.FirstOrDefault(cg => cg.Id == id && cg.IsActive);
            if (courseGroup == null) return null;

            return new CourseGroupResponseDto
            {
                Id = courseGroup.Id,
                Name = courseGroup.Name,
                Description = courseGroup.Description,
                DepartmentId = courseGroup.DepartmentId,
                DepartmentName = "Sample Department",
                IsActive = courseGroup.IsActive,
                CreatedAt = courseGroup.CreatedAt,
                CourseCount = 0
            };
        }

        public async Task UpdateCourseGroupAsync(int id, CourseGroupDto courseGroupDto)
        {
            var courseGroup = _courseGroups.FirstOrDefault(cg => cg.Id == id);
            if (courseGroup != null)
            {
                courseGroup.Name = courseGroupDto.Name;
                courseGroup.Description = courseGroupDto.Description;
                courseGroup.DepartmentId = courseGroupDto.DepartmentId;
                courseGroup.UpdatedAt = DateTime.UtcNow;
            }
        }

        public async Task DeleteCourseGroupAsync(int id)
        {
            var courseGroup = _courseGroups.FirstOrDefault(cg => cg.Id == id);
            if (courseGroup != null)
            {
                courseGroup.IsActive = false;
            }
        }
        #endregion

        private void InitializeDefaultRoles()
        {
            _roles.Add(new Role
            {
                Id = 1,
                Name = "Super Admin",
                Description = "Full system access with all permissions",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            _roles.Add(new Role
            {
                Id = 2,
                Name = "Placement Officer",
                Description = "Can manage placements and view reports",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            _roles.Add(new Role
            {
                Id = 3,
                Name = "Student",
                Description = "Can access courses and apply for jobs",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        private void InitializeDefaultPermissions()
        {
            // User Management Permissions
            _permissions.Add(new Permission
            {
                Id = 1,
                Name = "Create User",
                Description = "Can create new users",
                Module = "UserManagement",
                Action = "Create",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            _permissions.Add(new Permission
            {
                Id = 2,
                Name = "Read User",
                Description = "Can view user details",
                Module = "UserManagement",
                Action = "Read",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            _permissions.Add(new Permission
            {
                Id = 3,
                Name = "Update User",
                Description = "Can edit user information",
                Module = "UserManagement",
                Action = "Update",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            _permissions.Add(new Permission
            {
                Id = 4,
                Name = "Delete User",
                Description = "Can delete users",
                Module = "UserManagement",
                Action = "Delete",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            // Role Management Permissions
            _permissions.Add(new Permission
            {
                Id = 5,
                Name = "Create Role",
                Description = "Can create new roles",
                Module = "RoleManagement",
                Action = "Create",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            _permissions.Add(new Permission
            {
                Id = 6,
                Name = "Assign Permissions",
                Description = "Can assign permissions to roles",
                Module = "RoleManagement",
                Action = "Update",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            // Course Management Permissions
            _permissions.Add(new Permission
            {
                Id = 7,
                Name = "Create Course",
                Description = "Can create new courses",
                Module = "CourseManagement",
                Action = "Create",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            _permissions.Add(new Permission
            {
                Id = 8,
                Name = "Approve Course",
                Description = "Can approve courses",
                Module = "CourseManagement",
                Action = "Approve",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            // Placement Management Permissions
            _permissions.Add(new Permission
            {
                Id = 9,
                Name = "Manage Placements",
                Description = "Can manage job placements",
                Module = "PlacementManagement",
                Action = "Update",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            _permissions.Add(new Permission
            {
                Id = 10,
                Name = "View Reports",
                Description = "Can view placement reports",
                Module = "PlacementManagement",
                Action = "Read",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}