export interface Role {
  id: number;
  name: string;
  description: string;
  isActive: boolean;
  createdAt: Date;
  updatedAt: Date;
}

export interface Department {
  id: number;
  name: string;
  description: string;
  code?: string;
  institutionId?: number;
  isActive: boolean;
  createdAt: Date;
  updatedAt: Date;
}

export interface UserManagement {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  userType: string;
  isActive: boolean;
  createdAt: Date;
  roles: string[];
  departmentName?: string;
  institutionName?: string;
}

export interface RoleDto {
  name: string;
  description: string;
}

export interface DepartmentDto {
  name: string;
  description: string;
  code?: string;
  institutionId?: number;
}

export interface UserManagementDto {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: Date;
  address: string;
  userType: string;
  roleIds: number[];
}

export interface UserRoleAssignmentDto {
  userId: number;
  roleIds: number[];
}

export interface RoleResponseDto {
  id: number;
  name: string;
  description: string;
  isActive: boolean;
  createdAt: Date;
  userCount: number;
}

export interface DepartmentResponseDto {
  id: number;
  name: string;
  description: string;
  code?: string;
  institutionId?: number;
  institutionName?: string;
  isActive: boolean;
  createdAt: Date;
  studentCount: number;
}

export interface UserResponseDto {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  userType: string;
  isActive: boolean;
  createdAt: Date;
  roles: string[];
  departmentName?: string;
  institutionName?: string;
}

export interface AdminDashboardStats {
  totalUsers: number;
  totalRoles: number;
  totalDepartments: number;
  usersByType: {
    students: number;
    institutions: number;
    industries: number;
    admins: number;
  };
  recentActivity: Array<{
    action: string;
    timestamp: Date;
  }>;
}