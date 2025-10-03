import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, UserResponseDto, UserManagementDto, RoleResponseDto } from '../../../../core/services/admin.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: UserResponseDto[] = [];
  roles: RoleResponseDto[] = [];
  showUserForm = false;
  isEditing = false;
  currentUserId: number | null = null;

  userForm: FormGroup;
  roleAssignmentForm: FormGroup;

  userTypes = [
    'Admin',
    'Student',
    'Institution',
    'Industry',
    'Placement Officer'
  ];

  constructor(
    private adminService: AdminService,
    private fb: FormBuilder
  ) {
    this.userForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[\d\s\-\(\)]+$/)]],
      dateOfBirth: ['', Validators.required],
      address: ['', Validators.required],
      userType: ['', Validators.required],
      roleIds: [[]]
    });

    this.roleAssignmentForm = this.fb.group({
      userId: ['', Validators.required],
      roleIds: [[]]
    });
  }

  ngOnInit(): void {
    this.loadUsers();
    this.loadRoles();
  }

  loadUsers(): void {
    this.adminService.getAllUsers().subscribe({
      next: (users) => {
        this.users = users;
      },
      error: (error) => {
        console.error('Error loading users:', error);
      }
    });
  }

  loadRoles(): void {
    this.adminService.getAllRoles().subscribe({
      next: (roles) => {
        this.roles = roles;
      },
      error: (error) => {
        console.error('Error loading roles:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.userForm.valid) {
      const userData: UserManagementDto = this.userForm.value;

      if (this.isEditing && this.currentUserId) {
        this.adminService.updateUser(this.currentUserId, userData).subscribe({
          next: () => {
            this.loadUsers();
            this.resetForm();
          },
          error: (error) => {
            console.error('Error updating user:', error);
          }
        });
      } else {
        this.adminService.createUser(userData).subscribe({
          next: () => {
            this.loadUsers();
            this.resetForm();
          },
          error: (error) => {
            console.error('Error creating user:', error);
          }
        });
      }
    }
  }

  editUser(user: UserResponseDto): void {
    this.isEditing = true;
    this.currentUserId = user.id;
    this.showUserForm = true;

    this.userForm.patchValue({
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      phoneNumber: user.phoneNumber,
      dateOfBirth: user.createdAt.split('T')[0], // Format date for input
      address: '', // Would need to be added to UserResponseDto
      userType: user.userType,
      roleIds: [] // Would need to be populated from user's actual roles
    });
  }

  deleteUser(userId: number): void {
    if (confirm('Are you sure you want to delete this user?')) {
      this.adminService.deleteUser(userId).subscribe({
        next: () => {
          this.loadUsers();
        },
        error: (error) => {
          console.error('Error deleting user:', error);
        }
      });
    }
  }

  assignRoles(): void {
    if (this.roleAssignmentForm.valid) {
      this.adminService.assignUserRoles(this.roleAssignmentForm.value).subscribe({
        next: () => {
          this.loadUsers();
          this.roleAssignmentForm.reset();
        },
        error: (error) => {
          console.error('Error assigning roles:', error);
        }
      });
    }
  }

  resetForm(): void {
    this.userForm.reset();
    this.showUserForm = false;
    this.isEditing = false;
    this.currentUserId = null;
  }

  toggleUserForm(): void {
    this.showUserForm = !this.showUserForm;
    if (!this.showUserForm) {
      this.resetForm();
    }
  }

  updateRoleSelection(event: any, roleId: number): void {
    const roleIds = this.userForm.get('roleIds')?.value || [];
    if (event.target.checked) {
      roleIds.push(roleId);
    } else {
      const index = roleIds.indexOf(roleId);
      if (index > -1) {
        roleIds.splice(index, 1);
      }
    }
    this.userForm.patchValue({ roleIds });
  }

  updateRoleAssignment(event: any, roleId: number): void {
    const roleIds = this.roleAssignmentForm.get('roleIds')?.value || [];
    if (event.target.checked) {
      roleIds.push(roleId);
    } else {
      const index = roleIds.indexOf(roleId);
      if (index > -1) {
        roleIds.splice(index, 1);
      }
    }
    this.roleAssignmentForm.patchValue({ roleIds });
  }
}