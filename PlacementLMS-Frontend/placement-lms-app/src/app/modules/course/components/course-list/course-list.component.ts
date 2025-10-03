import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../../../core/services/course.service';
import { CourseResponseDto } from '../../../../core/models/course.model';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
  courses: CourseResponseDto[] = [];
  filteredCourses: CourseResponseDto[] = [];
  categories: string[] = ['Technical', 'Soft Skills', 'Domain-specific', 'All'];
  selectedCategory: string = 'All';
  isLoading = false;
  searchTerm = '';

  constructor(private courseService: CourseService) { }

  ngOnInit(): void {
    this.loadCourses();
  }

  loadCourses(): void {
    this.isLoading = true;
    this.courseService.getAllCourses().subscribe({
      next: (courses) => {
        this.courses = courses;
        this.filteredCourses = courses;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading courses:', error);
        this.isLoading = false;
      }
    });
  }

  filterByCategory(category: string): void {
    this.selectedCategory = category;
    this.applyFilters();
  }

  onSearch(): void {
    this.applyFilters();
  }

  private applyFilters(): void {
    let filtered = this.courses;

    // Filter by category
    if (this.selectedCategory !== 'All') {
      filtered = filtered.filter(course => course.category === this.selectedCategory);
    }

    // Filter by search term
    if (this.searchTerm.trim()) {
      const searchLower = this.searchTerm.toLowerCase();
      filtered = filtered.filter(course =>
        course.title.toLowerCase().includes(searchLower) ||
        course.description.toLowerCase().includes(searchLower) ||
        course.institutionName.toLowerCase().includes(searchLower)
      );
    }

    this.filteredCourses = filtered;
  }

  enrollInCourse(courseId: number): void {
    // Implementation for course enrollment
    console.log('Enroll in course:', courseId);
  }

  viewCourseDetails(courseId: number): void {
    // Implementation for viewing course details
    console.log('View course details:', courseId);
  }
}