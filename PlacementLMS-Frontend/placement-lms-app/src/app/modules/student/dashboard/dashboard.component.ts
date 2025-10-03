import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../../core/services/auth.service';
import { ApiService } from '../../../core/services/api.service';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  currentUser: any = null;
  dashboardData: any = null;
  isLoading = true;

  // Sample data for demonstration
  stats = {
    totalCourses: 5,
    completedCourses: 3,
    totalAssignments: 12,
    completedAssignments: 8,
    averageScore: 85.5,
    totalApplications: 7,
    interviewsScheduled: 2,
    certificatesEarned: 2
  };

  recentCourses = [
    {
      id: 1,
      title: 'Programming Fundamentals',
      progress: 85,
      status: 'In Progress',
      instructor: 'Dr. Smith',
      nextDeadline: '2024-10-15'
    },
    {
      id: 2,
      title: 'Data Structures & Algorithms',
      progress: 100,
      status: 'Completed',
      instructor: 'Prof. Johnson',
      nextDeadline: null
    },
    {
      id: 3,
      title: 'Web Development',
      progress: 45,
      status: 'In Progress',
      instructor: 'Ms. Davis',
      nextDeadline: '2024-10-20'
    }
  ];

  upcomingDeadlines = [
    {
      id: 1,
      title: 'Programming Assignment 3',
      course: 'Programming Fundamentals',
      dueDate: '2024-10-15',
      type: 'assignment'
    },
    {
      id: 2,
      title: 'Mock Test - Data Structures',
      course: 'Data Structures & Algorithms',
      dueDate: '2024-10-18',
      type: 'test'
    },
    {
      id: 3,
      title: 'Project Submission',
      course: 'Web Development',
      dueDate: '2024-10-20',
      type: 'project'
    }
  ];

  recentApplications = [
    {
      id: 1,
      company: 'Tech Solutions Inc.',
      position: 'Software Developer',
      appliedDate: '2024-10-01',
      status: 'Under Review'
    },
    {
      id: 2,
      company: 'Data Systems Corp',
      position: 'Junior Developer',
      appliedDate: '2024-09-28',
      status: 'Interview Scheduled'
    }
  ];

  constructor(
    private authService: AuthService,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    // Subscribe to current user changes
    this.authService.currentUser$.pipe(takeUntil(this.destroy$)).subscribe(user => {
      this.currentUser = user;
    });

    // Load dashboard data
    this.loadDashboardData();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadDashboardData(): void {
    this.isLoading = true;

    // Simulate API call
    setTimeout(() => {
      this.dashboardData = {
        stats: this.stats,
        recentCourses: this.recentCourses,
        upcomingDeadlines: this.upcomingDeadlines,
        recentApplications: this.recentApplications
      };
      this.isLoading = false;
    }, 1000);
  }

  // Get status badge class
  getStatusBadgeClass(status: string): string {
    switch (status.toLowerCase()) {
      case 'completed': return 'badge bg-success';
      case 'in progress': return 'badge bg-primary';
      case 'under review': return 'badge bg-warning';
      case 'interview scheduled': return 'badge bg-info';
      default: return 'badge bg-secondary';
    }
  }

  // Get progress bar color
  getProgressBarColor(progress: number): string {
    if (progress >= 80) return 'bg-success';
    if (progress >= 60) return 'bg-primary';
    if (progress >= 40) return 'bg-warning';
    return 'bg-danger';
  }

  // Get progress bar width
  getProgressBarWidth(progress: number): string {
    return `${Math.min(progress, 100)}%`;
  }

  // Navigate to course
  goToCourse(courseId: number): void {
    console.log('Navigate to course:', courseId);
    // Implementation would navigate to course details
  }

  // Navigate to assignments
  goToAssignments(): void {
    console.log('Navigate to assignments');
    // Implementation would navigate to assignments page
  }

  // Navigate to job applications
  goToApplications(): void {
    console.log('Navigate to job applications');
    // Implementation would navigate to applications page
  }

  // Quick action methods
  browseCourses(): void {
    console.log('Browse more courses');
    // Implementation would navigate to course catalog
  }

  buildResume(): void {
    console.log('Build resume');
    // Implementation would navigate to resume builder
  }

  findJobs(): void {
    console.log('Find job opportunities');
    // Implementation would navigate to job listings
  }

  takeMockTest(): void {
    console.log('Take mock test');
    // Implementation would navigate to mock tests
  }

  // Helper method to check if item is last in array
  isLastItem(array: any[], item: any): boolean {
    return array.indexOf(item) === array.length - 1;
  }
}