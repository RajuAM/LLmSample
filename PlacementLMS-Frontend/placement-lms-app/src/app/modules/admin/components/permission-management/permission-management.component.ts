import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, PermissionResponseDto, PermissionDto } from '../../../../core/services/admin.service';

@Component({
  selector: 'app-permission-management',
  templateUrl: './permission-management.component.html',
  styleUrls: ['./permission-management.component.css']
})
export class PermissionManagementComponent implements OnInit {
  permissions: PermissionResponseDto[] = [];
  showPermissionForm = false;
  isEditing = false;
  currentPermissionId: number | null = null;

  permissionForm: FormGroup;

  modules = [
    'UserManagement',
    'RoleManagement',
    'CourseManagement',
    'PlacementManagement',
    'SystemAdministration'
  ];

  actions = [
    'Create',
    'Read',
    'Update',
    'Delete',
    'Approve',
    'Manage'
  ];

  constructor(
    private adminService: AdminService,
    private fb: FormBuilder
  ) {
    this.permissionForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      module: ['', Validators.required],
      action: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadPermissions();
  }

  loadPermissions(): void {
    this.adminService.getAllPermissions().subscribe({
      next: (permissions) => {
        this.permissions = permissions;
      },
      error: (error) => {
        console.error('Error loading permissions:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.permissionForm.valid) {
      const permissionData: PermissionDto = this.permissionForm.value;

      this.adminService.createPermission(permissionData).subscribe({
        next: () => {
          this.loadPermissions();
          this.resetForm();
        },
        error: (error) => {
          console.error('Error creating permission:', error);
        }
      });
    }
  }

  resetForm(): void {
    this.permissionForm.reset();
    this.showPermissionForm = false;
    this.isEditing = false;
    this.currentPermissionId = null;
  }

  togglePermissionForm(): void {
    this.showPermissionForm = !this.showPermissionForm;
    if (!this.showPermissionForm) {
      this.resetForm();
    }
  }
}