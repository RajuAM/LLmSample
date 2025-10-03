import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  currentUser: any = null;
  userInitials: string = '';
  isAuthenticated: boolean = false;
  showDropdown: boolean = false;
  isMobileMenuOpen: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Subscribe to authentication state changes
    this.authService.currentUser$.pipe(takeUntil(this.destroy$)).subscribe(user => {
      this.currentUser = user;
      this.userInitials = this.authService.getUserInitials();
      this.isAuthenticated = this.authService.isAuthenticated();
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  // Toggle user dropdown menu
  toggleDropdown(): void {
    this.showDropdown = !this.showDropdown;
  }

  // Toggle mobile menu
  toggleMobileMenu(): void {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

  // Logout user
  logout(): void {
    this.authService.logout();
    this.showDropdown = false;
    this.isMobileMenuOpen = false;
  }

  // Navigate to profile
  goToProfile(): void {
    const userType = this.currentUser?.userType?.toLowerCase();
    switch (userType) {
      case 'admin':
        this.router.navigate(['/admin/profile']);
        break;
      case 'student':
        this.router.navigate(['/student/profile']);
        break;
      case 'institution':
        this.router.navigate(['/institution/profile']);
        break;
      case 'industry':
        this.router.navigate(['/industry/profile']);
        break;
      default:
        this.router.navigate(['/profile']);
    }
    this.showDropdown = false;
  }

  // Navigate to dashboard
  goToDashboard(): void {
    const userType = this.currentUser?.userType?.toLowerCase();
    switch (userType) {
      case 'admin':
        this.router.navigate(['/admin/dashboard']);
        break;
      case 'student':
        this.router.navigate(['/student/dashboard']);
        break;
      case 'institution':
        this.router.navigate(['/institution/dashboard']);
        break;
      case 'industry':
        this.router.navigate(['/industry/dashboard']);
        break;
      default:
        this.router.navigate(['/dashboard']);
    }
    this.showDropdown = false;
    this.isMobileMenuOpen = false;
  }

  // Get navigation items based on user role
  getNavItems() {
    const userType = this.currentUser?.userType?.toLowerCase();

    switch (userType) {
      case 'admin':
        return [
          { label: 'Dashboard', route: '/admin/dashboard', icon: 'fas fa-tachometer-alt' },
          { label: 'Users', route: '/admin/users', icon: 'fas fa-users' },
          { label: 'Roles', route: '/admin/roles', icon: 'fas fa-user-tag' },
          { label: 'Departments', route: '/admin/departments', icon: 'fas fa-building' },
          { label: 'Reports', route: '/admin/reports', icon: 'fas fa-chart-bar' }
        ];
      case 'student':
        return [
          { label: 'Dashboard', route: '/student/dashboard', icon: 'fas fa-tachometer-alt' },
          { label: 'Courses', route: '/student/courses', icon: 'fas fa-book' },
          { label: 'Assignments', route: '/student/assignments', icon: 'fas fa-tasks' },
          { label: 'Mock Tests', route: '/student/tests', icon: 'fas fa-clipboard-list' },
          { label: 'Resume', route: '/student/resume', icon: 'fas fa-file-alt' },
          { label: 'Jobs', route: '/student/jobs', icon: 'fas fa-briefcase' }
        ];
      case 'institution':
        return [
          { label: 'Dashboard', route: '/institution/dashboard', icon: 'fas fa-tachometer-alt' },
          { label: 'Courses', route: '/institution/courses', icon: 'fas fa-book' },
          { label: 'Students', route: '/institution/students', icon: 'fas fa-users' },
          { label: 'Assignments', route: '/institution/assignments', icon: 'fas fa-tasks' },
          { label: 'Reports', route: '/institution/reports', icon: 'fas fa-chart-bar' }
        ];
      case 'industry':
        return [
          { label: 'Dashboard', route: '/industry/dashboard', icon: 'fas fa-tachometer-alt' },
          { label: 'Job Postings', route: '/industry/jobs', icon: 'fas fa-bullhorn' },
          { label: 'Applications', route: '/industry/applications', icon: 'fas fa-file-import' },
          { label: 'Interviews', route: '/industry/interviews', icon: 'fas fa-calendar-alt' },
          { label: 'Analytics', route: '/industry/analytics', icon: 'fas fa-chart-line' }
        ];
      default:
        return [];
    }
  }

  // Check if current route is active
  isActive(route: string): boolean {
    return this.router.url.includes(route);
  }

  // Get user display name
  getUserDisplayName(): string {
    if (this.currentUser) {
      return `${this.currentUser.firstName} ${this.currentUser.lastName}`;
    }
    return 'User';
  }
}