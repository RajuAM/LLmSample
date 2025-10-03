import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, CourseGroupResponseDto, CourseGroupDto } from '../../../../core/services/admin.service';

@Component({
  selector: 'app-course-group-management',
  templateUrl: './course-group-management.component.html',
  styleUrls: ['./course-group-management.component.css']
})
export class CourseGroupManagementComponent implements OnInit {
  courseGroups: CourseGroupResponseDto[] = [];
  showCourseGroupForm = false;
  isEditing = false;
  currentCourseGroupId: number | null = null;

  courseGroupForm: FormGroup;

  departments = [
    { id: 1, name: 'Computer Science' },
    { id: 2, name: 'Information Technology' },
    { id: 3, name: 'Electronics' },
    { id: 4, name: 'Mechanical' }
  ];

  constructor(
    private adminService: AdminService,
    private fb: FormBuilder
  ) {
    this.courseGroupForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      departmentId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadCourseGroups();
  }

  loadCourseGroups(): void {
    this.adminService.getAllCourseGroups().subscribe({
      next: (courseGroups) => {
        this.courseGroups = courseGroups;
      },
      error: (error) => {
        console.error('Error loading course groups:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.courseGroupForm.valid) {
      const courseGroupData: CourseGroupDto = this.courseGroupForm.value;

      if (this.isEditing && this.currentCourseGroupId) {
        this.adminService.updateCourseGroup(this.currentCourseGroupId, courseGroupData).subscribe({
          next: () => {
            this.loadCourseGroups();
            this.resetForm();
          },
          error: (error) => {
            console.error('Error updating course group:', error);
          }
        });
      } else {
        this.adminService.createCourseGroup(courseGroupData).subscribe({
          next: () => {
            this.loadCourseGroups();
            this.resetForm();
          },
          error: (error) => {
            console.error('Error creating course group:', error);
          }
        });
      }
    }
  }

  editCourseGroup(courseGroup: CourseGroupResponseDto): void {
    this.isEditing = true;
    this.currentCourseGroupId = courseGroup.id;
    this.showCourseGroupForm = true;

    this.courseGroupForm.patchValue({
      name: courseGroup.name,
      description: courseGroup.description,
      departmentId: courseGroup.departmentId
    });
  }

  deleteCourseGroup(courseGroupId: number): void {
    if (confirm('Are you sure you want to delete this course group?')) {
      this.adminService.deleteCourseGroup(courseGroupId).subscribe({
        next: () => {
          this.loadCourseGroups();
        },
        error: (error) => {
          console.error('Error deleting course group:', error);
        }
      });
    }
  }

  resetForm(): void {
    this.courseGroupForm.reset();
    this.showCourseGroupForm = false;
    this.isEditing = false;
    this.currentCourseGroupId = null;
  }

  toggleCourseGroupForm(): void {
    this.showCourseGroupForm = !this.showCourseGroupForm;
    if (!this.showCourseGroupForm) {
      this.resetForm();
    }
  }
}