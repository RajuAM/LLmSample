import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ApiService } from './api.service';
import { User, LoginRequest, RegisterRequest, AuthResponse } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  private tokenSubject = new BehaviorSubject<string | null>(null);
  public token$ = this.tokenSubject.asObservable();

  constructor(
    private apiService: ApiService,
    private router: Router
  ) {
    // Check if user is already logged in on service initialization
    const token = localStorage.getItem('token');
    const user = localStorage.getItem('currentUser');

    if (token && user) {
      this.tokenSubject.next(token);
      this.currentUserSubject.next(JSON.parse(user));
    }
  }

  // Login method
  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.apiService.post<AuthResponse>('/auth/login', credentials)
      .pipe(
        map(response => {
          if (response.token) {
            // Store token and user data
            localStorage.setItem('token', response.token);
            localStorage.setItem('currentUser', JSON.stringify(response.user));

            // Update subjects
            this.tokenSubject.next(response.token);
            this.currentUserSubject.next(response.user);
          }
          return response;
        })
      );
  }

  // Register method
  register(userData: RegisterRequest): Observable<any> {
    return this.apiService.post<any>('/auth/register', userData);
  }

  // Logout method
  logout(): void {
    // Remove stored data
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');

    // Update subjects
    this.tokenSubject.next(null);
    this.currentUserSubject.next(null);

    // Navigate to login
    this.router.navigate(['/auth/login']);
  }

  // Get current user
  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  // Get current token
  getToken(): string | null {
    return this.tokenSubject.value;
  }

  // Check if user is authenticated
  isAuthenticated(): boolean {
    return this.getToken() !== null;
  }

  // Check if current user has specific role
  hasRole(role: string): boolean {
    const user = this.getCurrentUser();
    return user?.userType?.toLowerCase() === role.toLowerCase();
  }

  // Check if current user has any of the specified roles
  hasAnyRole(roles: string[]): boolean {
    const user = this.getCurrentUser();
    return user ? roles.some(role => user.userType?.toLowerCase() === role.toLowerCase()) : false;
  }

  // Get user display name
  getUserDisplayName(): string {
    const user = this.getCurrentUser();
    return user ? `${user.firstName} ${user.lastName}` : '';
  }

  // Get user initials for avatar
  getUserInitials(): string {
    const user = this.getCurrentUser();
    if (user) {
      return `${user.firstName.charAt(0)}${user.lastName.charAt(0)}`.toUpperCase();
    }
    return 'U';
  }
}