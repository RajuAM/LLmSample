import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { ApiService } from './api.service';
import {
  DashboardDto,
  DashboardSummaryDto,
  StudentAnalyticsDto,
  CourseAnalyticsDto,
  PlacementAnalyticsDto,
  CertificateAnalyticsDto,
  StudentReportDto,
  CourseReportDto,
  PlacementReportDto,
  CertificateReportDto,
  ProgressReportDto,
  ReportFiltersDto,
  MonthlyRegistrationDto,
  MonthlyEnrollmentDto,
  MonthlyPlacementDto,
  ExportOptions
} from '../models/dashboard.model';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private readonly dashboardEndpoint = '/dashboard';

  constructor(private apiService: ApiService) { }

  // Dashboard Overview
  getDashboardData(): Observable<DashboardDto> {
    return this.apiService.get<DashboardDto>(`${this.dashboardEndpoint}/overview`);
  }

  getDashboardSummary(): Observable<DashboardSummaryDto> {
    return this.apiService.get<DashboardSummaryDto>(`${this.dashboardEndpoint}/summary`);
  }

  // Student Analytics
  getStudentAnalytics(): Observable<StudentAnalyticsDto> {
    return this.apiService.get<StudentAnalyticsDto>(`${this.dashboardEndpoint}/students`);
  }

  getStudentRegistrationTrends(months: number = 12): Observable<MonthlyRegistrationDto[]> {
    return this.apiService.get<MonthlyRegistrationDto[]>(`${this.dashboardEndpoint}/students/trends?months=${months}`);
  }

  getStudentReport(filters: ReportFiltersDto): Observable<StudentReportDto> {
    let query = '';
    const params = [];

    if (filters.startDate) params.push(`startDate=${filters.startDate.toISOString()}`);
    if (filters.endDate) params.push(`endDate=${filters.endDate.toISOString()}`);
    if (filters.department) params.push(`department=${filters.department}`);
    if (filters.semester) params.push(`semester=${filters.semester}`);

    if (params.length > 0) query = `?${params.join('&')}`;

    return this.apiService.get<StudentReportDto>(`${this.dashboardEndpoint}/students/report${query}`);
  }

  // Course Analytics
  getCourseAnalytics(): Observable<CourseAnalyticsDto> {
    return this.apiService.get<CourseAnalyticsDto>(`${this.dashboardEndpoint}/courses`);
  }

  getCourseEnrollmentTrends(months: number = 12): Observable<MonthlyEnrollmentDto[]> {
    return this.apiService.get<MonthlyEnrollmentDto[]>(`${this.dashboardEndpoint}/courses/trends?months=${months}`);
  }

  getCourseReport(filters: ReportFiltersDto): Observable<CourseReportDto> {
    let query = '';
    const params = [];

    if (filters.startDate) params.push(`startDate=${filters.startDate.toISOString()}`);
    if (filters.endDate) params.push(`endDate=${filters.endDate.toISOString()}`);
    if (filters.courseCategory) params.push(`courseCategory=${filters.courseCategory}`);

    if (params.length > 0) query = `?${params.join('&')}`;

    return this.apiService.get<CourseReportDto>(`${this.dashboardEndpoint}/courses/report${query}`);
  }

  // Placement Analytics
  getPlacementAnalytics(): Observable<PlacementAnalyticsDto> {
    return this.apiService.get<PlacementAnalyticsDto>(`${this.dashboardEndpoint}/placements`);
  }

  getPlacementTrends(months: number = 12): Observable<MonthlyPlacementDto[]> {
    return this.apiService.get<MonthlyPlacementDto[]>(`${this.dashboardEndpoint}/placements/trends?months=${months}`);
  }

  getPlacementReport(filters: ReportFiltersDto): Observable<PlacementReportDto> {
    let query = '';
    const params = [];

    if (filters.startDate) params.push(`startDate=${filters.startDate.toISOString()}`);
    if (filters.endDate) params.push(`endDate=${filters.endDate.toISOString()}`);
    if (filters.industry) params.push(`industry=${filters.industry}`);

    if (params.length > 0) query = `?${params.join('&')}`;

    return this.apiService.get<PlacementReportDto>(`${this.dashboardEndpoint}/placements/report${query}`);
  }

  // Certificate Analytics
  getCertificateAnalytics(): Observable<CertificateAnalyticsDto> {
    return this.apiService.get<CertificateAnalyticsDto>(`${this.dashboardEndpoint}/certificates`);
  }

  getCertificateReport(filters: ReportFiltersDto): Observable<CertificateReportDto> {
    let query = '';
    const params = [];

    if (filters.startDate) params.push(`startDate=${filters.startDate.toISOString()}`);
    if (filters.endDate) params.push(`endDate=${filters.endDate.toISOString()}`);
    if (filters.courseCategory) params.push(`courseCategory=${filters.courseCategory}`);

    if (params.length > 0) query = `?${params.join('&')}`;

    return this.apiService.get<CertificateReportDto>(`${this.dashboardEndpoint}/certificates/report${query}`);
  }

  // Progress Analytics
  getProgressAnalytics(): Observable<ProgressReportDto> {
    return this.apiService.get<ProgressReportDto>(`${this.dashboardEndpoint}/progress`);
  }

  getProgressReport(filters: ReportFiltersDto): Observable<ProgressReportDto> {
    let query = '';
    const params = [];

    if (filters.startDate) params.push(`startDate=${filters.startDate.toISOString()}`);
    if (filters.endDate) params.push(`endDate=${filters.endDate.toISOString()}`);
    if (filters.department) params.push(`department=${filters.department}`);
    if (filters.semester) params.push(`semester=${filters.semester}`);

    if (params.length > 0) query = `?${params.join('&')}`;

    return this.apiService.get<ProgressReportDto>(`${this.dashboardEndpoint}/progress/report${query}`);
  }

  // Export Methods (Simplified for current API structure)
  exportStudentReport(filters: ReportFiltersDto, format: string = 'excel'): Observable<any> {
    let query = `?format=${format}`;
    const params = [];

    if (filters.startDate) params.push(`startDate=${filters.startDate.toISOString()}`);
    if (filters.endDate) params.push(`endDate=${filters.endDate.toISOString()}`);
    if (filters.department) params.push(`department=${filters.department}`);
    if (filters.semester) params.push(`semester=${filters.semester}`);

    if (params.length > 0) query += `&${params.join('&')}`;

    return this.apiService.get<any>(`${this.dashboardEndpoint}/export/students${query}`);
  }

  exportCourseReport(filters: ReportFiltersDto, format: string = 'excel'): Observable<any> {
    let query = `?format=${format}`;
    const params = [];

    if (filters.startDate) params.push(`startDate=${filters.startDate.toISOString()}`);
    if (filters.endDate) params.push(`endDate=${filters.endDate.toISOString()}`);
    if (filters.courseCategory) params.push(`courseCategory=${filters.courseCategory}`);

    if (params.length > 0) query += `&${params.join('&')}`;

    return this.apiService.get<any>(`${this.dashboardEndpoint}/export/courses${query}`);
  }

  exportPlacementReport(filters: ReportFiltersDto, format: string = 'excel'): Observable<any> {
    let query = `?format=${format}`;
    const params = [];

    if (filters.startDate) params.push(`startDate=${filters.startDate.toISOString()}`);
    if (filters.endDate) params.push(`endDate=${filters.endDate.toISOString()}`);
    if (filters.industry) params.push(`industry=${filters.industry}`);

    if (params.length > 0) query += `&${params.join('&')}`;

    return this.apiService.get<any>(`${this.dashboardEndpoint}/export/placements${query}`);
  }

  exportCertificateReport(filters: ReportFiltersDto, format: string = 'excel'): Observable<any> {
    let query = `?format=${format}`;
    const params = [];

    if (filters.startDate) params.push(`startDate=${filters.startDate.toISOString()}`);
    if (filters.endDate) params.push(`endDate=${filters.endDate.toISOString()}`);
    if (filters.courseCategory) params.push(`courseCategory=${filters.courseCategory}`);

    if (params.length > 0) query += `&${params.join('&')}`;

    return this.apiService.get<any>(`${this.dashboardEndpoint}/export/certificates${query}`);
  }

  // Real-time Data
  getRealTimeSummary(): Observable<DashboardSummaryDto> {
    return this.apiService.get<DashboardSummaryDto>(`${this.dashboardEndpoint}/realtime/summary`);
  }

  getRealTimeActivity(): Observable<any> {
    return this.apiService.get<any>(`${this.dashboardEndpoint}/realtime/activity`);
  }

  // Utility Methods
  getDefaultFilters(): ReportFiltersDto {
    return {
      startDate: new Date(Date.now() - 30 * 24 * 60 * 60 * 1000), // Last 30 days
      endDate: new Date()
    };
  }

  getDepartmentList(): string[] {
    return [
      'Computer Science',
      'Information Technology',
      'Electronics',
      'Mechanical',
      'Civil',
      'Electrical',
      'Chemical',
      'Biotechnology',
      'Management',
      'Commerce'
    ];
  }

  getSemesterList(): number[] {
    return [1, 2, 3, 4, 5, 6, 7, 8];
  }

  getCourseCategories(): string[] {
    return [
      'Technical',
      'Soft Skills',
      'Domain-specific',
      'Programming',
      'Data Science',
      'Machine Learning',
      'Web Development',
      'Mobile Development',
      'Cloud Computing',
      'Cybersecurity'
    ];
  }

  getIndustries(): string[] {
    return [
      'Information Technology',
      'Finance',
      'Healthcare',
      'Education',
      'Manufacturing',
      'Retail',
      'Consulting',
      'Telecommunications',
      'Energy',
      'Transportation'
    ];
  }

  getExportFormats(): string[] {
    return ['excel', 'pdf', 'csv'];
  }
}