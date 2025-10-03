import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, RoleResponseDto, RoleDto } from '../../../../core/services/admin.service';

@Component({
  selector: 'app-role-management',
  templateUrl: './role-management.component.html',
  styleUrls: ['./role-management.component.css']
})
export class RoleManagementComponent implements OnInit {
  roles: RoleResponseDto[] = [];
  showRoleForm = false;
  isEditing = false;
  currentRoleId: number | null = null;

  roleForm: FormGroup;

  constructor(
    private adminService: AdminService,
    private fb: FormBuilder
  ) {
    this.roleForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: ['', [Validators.required, Validators.minLength(10)]]
    });
  }

  ngOnInit(): void {
    this.loadRoles();
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
    if (this.roleForm.valid) {
      const roleData: RoleDto = this.roleForm.value;

      if (this.isEditing && this.currentRoleId) {
        this.adminService.updateRole(this.currentRoleId, roleData).subscribe({
          next: () => {
            this.loadRoles();
            this.resetForm();
          },
          error: (error) => {
            console.error('Error updating role:', error);
          }
        });
      } else {
        this.adminService.createRole(roleData).subscribe({
          next: () => {
            this.loadRoles();
            this.resetForm();
          },
          error: (error) => {
            console.error('Error creating role:', error);
          }
        });
      }
    }
  }

  editRole(role: RoleResponseDto): void {
    this.isEditing = true;
    this.currentRoleId = role.id;
    this.showRoleForm = true;

    this.roleForm.patchValue({
      name: role.name,
      description: role.description
    });
  }

  deleteRole(roleId: number): void {
    if (confirm('Are you sure you want to delete this role?')) {
      this.adminService.deleteRole(roleId).subscribe({
        next: () => {
          this.loadRoles();
        },
        error: (error) => {
          console.error('Error deleting role:', error);
        }
      });
    }
  }

  resetForm(): void {
    this.roleForm.reset();
    this.showRoleForm = false;
    this.isEditing = false;
    this.currentRoleId = null;
  }

  toggleRoleForm(): void {
    this.showRoleForm = !this.showRoleForm;
    if (!this.showRoleForm) {
      this.resetForm();
    }
  }
}