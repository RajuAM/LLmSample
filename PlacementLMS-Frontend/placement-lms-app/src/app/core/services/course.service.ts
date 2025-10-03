import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import {
  Course,
  CourseDto,
  CourseResponseDto,
  CourseEnrollmentDto,
  Assignment,
  AssignmentDto,
  AssignmentResponseDto,
  StudentAssignmentDto,
  CourseAnalyticsDto
} from '../models/course.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private readonly endpoint = '/course';

  constructor(private apiService: ApiService) { }

  // Course Management
  getAllCourses(): Observable<CourseResponseDto[]> {
    return this.apiService.get<CourseResponseDto[]>(`${this.endpoint}`);
  }

  getCourseById(courseId: number): Observable<CourseResponseDto> {
    return this.apiService.get<CourseResponseDto>(`${this.endpoint}/${courseId}`);
  }

  createCourse(courseDto: CourseDto): Observable<Course> {
    return this.apiService.post<Course>(`${this.endpoint}`, courseDto);
  }

  updateCourse(courseId: number, courseDto: CourseDto): Observable<void> {
    return this.apiService.put<void>(`${this.endpoint}/${courseId}`, courseDto);
  }

  deleteCourse(courseId: number): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${courseId}`);
  }

  getCoursesByInstitution(institutionId: number): Observable<CourseResponseDto[]> {
    return this.apiService.get<CourseResponseDto[]>(`${this.endpoint}/institution/${institutionId}`);
  }

  getCoursesByCategory(category: string): Observable<CourseResponseDto[]> {
    return this.apiService.get<CourseResponseDto[]>(`${this.endpoint}/category/${category}`);
  }

  // Assignment Management
  getCourseAssignments(courseId: number): Observable<AssignmentResponseDto[]> {
    return this.apiService.get<AssignmentResponseDto[]>(`${this.endpoint}/${courseId}/assignments`);
  }

  getAssignmentById(assignmentId: number): Observable<AssignmentResponseDto> {
    return this.apiService.get<AssignmentResponseDto>(`${this.endpoint}/assignments/${assignmentId}`);
  }

  createAssignment(assignmentDto: AssignmentDto): Observable<Assignment> {
    return this.apiService.post<Assignment>(`${this.endpoint}/assignments`, assignmentDto);
  }

  updateAssignment(assignmentId: number, assignmentDto: AssignmentDto): Observable<void> {
    return this.apiService.put<void>(`${this.endpoint}/assignments/${assignmentId}`, assignmentDto);
  }

  deleteAssignment(assignmentId: number): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/assignments/${assignmentId}`);
  }

  // Content Management
  getCourseContent(courseId: number): Observable<{ courseId: number; videoUrl?: string; pdfUrl?: string }> {
    return this.apiService.get<{ courseId: number; videoUrl?: string; pdfUrl?: string }>(`${this.endpoint}/${courseId}/content`);
  }

  updateCourseContent(courseId: number, videoPath?: string, pdfPath?: string): Observable<{ message: string }> {
    return this.apiService.put<{ message: string }>(`${this.endpoint}/${courseId}/content`, {
      videoPath,
      pdfPath
    });
  }

  // Enrollment Management
  enrollInCourse(enrollmentDto: CourseEnrollmentDto): Observable<{ message: string; enrollmentId: number }> {
    return this.apiService.post<{ message: string; enrollmentId: number }>(`${this.endpoint}/enroll`, enrollmentDto);
  }

  getEnrolledCourses(): Observable<CourseResponseDto[]> {
    return this.apiService.get<CourseResponseDto[]>(`${this.endpoint}/enrolled`);
  }

  getCourseProgress(courseId: number): Observable<{ progress: number; status: string }> {
    return this.apiService.get<{ progress: number; status: string }>(`${this.endpoint}/${courseId}/progress`);
  }

  // Assignment Submission
  submitAssignment(submissionDto: StudentAssignmentDto): Observable<{ message: string; submissionId: number }> {
    return this.apiService.post<{ message: string; submissionId: number }>(`${this.endpoint}/assignments/submit`, submissionDto);
  }

  getStudentAssignments(): Observable<StudentAssignmentDto[]> {
    return this.apiService.get<StudentAssignmentDto[]>(`${this.endpoint}/assignments`);
  }

  // Analytics
  getCourseAnalytics(courseId: number): Observable<CourseAnalyticsDto> {
    return this.apiService.get<CourseAnalyticsDto>(`${this.endpoint}/${courseId}/analytics`);
  }

  getCourseEnrollments(courseId: number): Observable<any[]> {
    return this.apiService.get<any[]>(`${this.endpoint}/${courseId}/enrollments`);
  }
}