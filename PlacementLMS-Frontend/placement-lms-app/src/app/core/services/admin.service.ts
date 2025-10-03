import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';

export interface RoleDto {
  name: string;
  description: string;
}

export interface RoleResponseDto {
  id: number;
  name: string;
  description: string;
  isActive: boolean;
  createdAt: string;
  userCount: number;
}

export interface PermissionDto {
  name: string;
  description: string;
  module: string;
  action: string;
}

export interface PermissionResponseDto {
  id: number;
  name: string;
  description: string;
  module: string;
  action: string;
  isActive: boolean;
  createdAt: string;
  roleCount: number;
}

export interface CourseGroupDto {
  name: string;
  description: string;
  departmentId: number;
}

export interface CourseGroupResponseDto {
  id: number;
  name: string;
  description: string;
  departmentId: number;
  departmentName: string;
  isActive: boolean;
  createdAt: string;
  courseCount: number;
}

export interface UserManagementDto {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: string;
  address: string;
  userType: string;
  roleIds: number[];
}

export interface UserResponseDto {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  userType: string;
  isActive: boolean;
  createdAt: string;
  roles: string[];
  departmentName: string;
  institutionName: string;
}

export interface UserRoleAssignmentDto {
  userId: number;
  roleIds: number[];
}

export interface RolePermissionAssignmentDto {
  roleId: number;
  permissionIds: number[];
}

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private apiService: ApiService) { }

  // Role Management
  createRole(roleDto: RoleDto): Observable<RoleResponseDto> {
    return this.apiService.post<RoleResponseDto>('/admin/roles', roleDto);
  }

  getAllRoles(): Observable<RoleResponseDto[]> {
    return this.apiService.get<RoleResponseDto[]>('/admin/roles');
  }

  getRoleById(id: number): Observable<RoleResponseDto> {
    return this.apiService.get<RoleResponseDto>(`/admin/roles/${id}`);
  }

  updateRole(id: number, roleDto: RoleDto): Observable<void> {
    return this.apiService.put<void>(`/admin/roles/${id}`, roleDto);
  }

  deleteRole(id: number): Observable<void> {
    return this.apiService.delete<void>(`/admin/roles/${id}`);
  }

  // Permission Management
  createPermission(permissionDto: PermissionDto): Observable<PermissionResponseDto> {
    return this.apiService.post<PermissionResponseDto>('/admin/permissions', permissionDto);
  }

  getAllPermissions(): Observable<PermissionResponseDto[]> {
    return this.apiService.get<PermissionResponseDto[]>('/admin/permissions');
  }

  getPermissionById(id: number): Observable<PermissionResponseDto> {
    return this.apiService.get<PermissionResponseDto>(`/admin/permissions/${id}`);
  }

  assignRolePermissions(assignmentDto: RolePermissionAssignmentDto): Observable<any> {
    return this.apiService.post<any>('/admin/roles/assign-permissions', assignmentDto);
  }

  // Course Group Management
  createCourseGroup(courseGroupDto: CourseGroupDto): Observable<CourseGroupResponseDto> {
    return this.apiService.post<CourseGroupResponseDto>('/admin/course-groups', courseGroupDto);
  }

  getAllCourseGroups(): Observable<CourseGroupResponseDto[]> {
    return this.apiService.get<CourseGroupResponseDto[]>('/admin/course-groups');
  }

  getCourseGroupById(id: number): Observable<CourseGroupResponseDto> {
    return this.apiService.get<CourseGroupResponseDto>(`/admin/course-groups/${id}`);
  }

  updateCourseGroup(id: number, courseGroupDto: CourseGroupDto): Observable<void> {
    return this.apiService.put<void>(`/admin/course-groups/${id}`, courseGroupDto);
  }

  deleteCourseGroup(id: number): Observable<void> {
    return this.apiService.delete<void>(`/admin/course-groups/${id}`);
  }

  // User Management
  createUser(userDto: UserManagementDto): Observable<UserResponseDto> {
    return this.apiService.post<UserResponseDto>('/admin/users', userDto);
  }

  getAllUsers(): Observable<UserResponseDto[]> {
    return this.apiService.get<UserResponseDto[]>('/admin/users');
  }

  getUserById(id: number): Observable<UserResponseDto> {
    return this.apiService.get<UserResponseDto>(`/admin/users/${id}`);
  }

  updateUser(id: number, userDto: UserManagementDto): Observable<void> {
    return this.apiService.put<void>(`/admin/users/${id}`, userDto);
  }

  deleteUser(id: number): Observable<void> {
    return this.apiService.delete<void>(`/admin/users/${id}`);
  }

  assignUserRoles(assignmentDto: UserRoleAssignmentDto): Observable<any> {
    return this.apiService.post<any>('/admin/users/assign-roles', assignmentDto);
  }

  getUsersByType(userType: string): Observable<UserResponseDto[]> {
    return this.apiService.get<UserResponseDto[]>(`/admin/users/type/${userType}`);
  }
}