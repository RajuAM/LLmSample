import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import {
  JobOpportunity,
  JobOpportunityDto,
  JobOpportunityResponseDto,
  JobApplication,
  JobApplicationDto,
  JobApplicationResponseDto,
  Company,
  CompanyRegistrationDto,
  CompanyUpdateDto,
  ApplicationStatusDto,
  InterviewScheduleDto,
  PlacementStatsDto
} from '../models/placement.model';

@Injectable({
  providedIn: 'root'
})
export class PlacementService {
  private readonly companyEndpoint = '/company';
  private readonly studentEndpoint = '/student';

  constructor(private apiService: ApiService) { }

  // Company Management
  registerCompany(companyDto: CompanyRegistrationDto): Observable<Company> {
    return this.apiService.post<Company>(`${this.companyEndpoint}/register`, companyDto);
  }

  getCompanyById(companyId: number): Observable<Company> {
    return this.apiService.get<Company>(`${this.companyEndpoint}/${companyId}`);
  }

  updateCompany(companyId: number, companyDto: CompanyUpdateDto): Observable<void> {
    return this.apiService.put<void>(`${this.companyEndpoint}/${companyId}`, companyDto);
  }

  getCompanyProfile(): Observable<Company> {
    return this.apiService.get<Company>(`${this.companyEndpoint}/profile`);
  }

  // Job Opportunity Management
  createJobOpportunity(jobDto: JobOpportunityDto): Observable<JobOpportunity> {
    return this.apiService.post<JobOpportunity>(`${this.companyEndpoint}/jobs`, jobDto);
  }

  getCompanyJobOpportunities(): Observable<JobOpportunityResponseDto[]> {
    return this.apiService.get<JobOpportunityResponseDto[]>(`${this.companyEndpoint}/jobs`);
  }

  getJobOpportunityById(jobId: number): Observable<JobOpportunityResponseDto> {
    return this.apiService.get<JobOpportunityResponseDto>(`${this.companyEndpoint}/jobs/${jobId}`);
  }

  updateJobOpportunity(jobId: number, jobDto: JobOpportunityDto): Observable<void> {
    return this.apiService.put<void>(`${this.companyEndpoint}/jobs/${jobId}`, jobDto);
  }

  deleteJobOpportunity(jobId: number): Observable<void> {
    return this.apiService.delete<void>(`${this.companyEndpoint}/jobs/${jobId}`);
  }

  // Application Management
  getJobApplications(jobId: number): Observable<JobApplicationResponseDto[]> {
    return this.apiService.get<JobApplicationResponseDto[]>(`${this.companyEndpoint}/jobs/${jobId}/applications`);
  }

  updateApplicationStatus(jobId: number, applicationId: number, statusDto: ApplicationStatusDto): Observable<{ message: string }> {
    return this.apiService.put<{ message: string }>(`${this.companyEndpoint}/jobs/${jobId}/applications/${applicationId}/status`, statusDto);
  }

  scheduleInterview(jobId: number, applicationId: number, scheduleDto: InterviewScheduleDto): Observable<{ message: string }> {
    return this.apiService.post<{ message: string }>(`${this.companyEndpoint}/jobs/${jobId}/applications/${applicationId}/schedule-interview`, scheduleDto);
  }

  getPendingApplications(): Observable<JobApplicationResponseDto[]> {
    return this.apiService.get<JobApplicationResponseDto[]>(`${this.companyEndpoint}/applications/pending`);
  }

  getShortlistedApplications(): Observable<JobApplicationResponseDto[]> {
    return this.apiService.get<JobApplicationResponseDto[]>(`${this.companyEndpoint}/applications/shortlisted`);
  }

  // Analytics
  getCompanyAnalytics(): Observable<PlacementStatsDto> {
    return this.apiService.get<PlacementStatsDto>(`${this.companyEndpoint}/analytics`);
  }

  getJobAnalytics(jobId: number): Observable<JobOpportunityResponseDto> {
    return this.apiService.get<JobOpportunityResponseDto>(`${this.companyEndpoint}/jobs/${jobId}/analytics`);
  }

  // Student-side Job Management
  getAvailableJobOpportunities(): Observable<JobOpportunityResponseDto[]> {
    return this.apiService.get<JobOpportunityResponseDto[]>(`${this.studentEndpoint}/jobs/available`);
  }

  getJobOpportunityByIdForStudent(jobId: number): Observable<JobOpportunityResponseDto> {
    return this.apiService.get<JobOpportunityResponseDto>(`${this.studentEndpoint}/jobs/${jobId}`);
  }

  applyForJob(applicationDto: JobApplicationDto): Observable<{ message: string }> {
    return this.apiService.post<{ message: string }>(`${this.studentEndpoint}/jobs/apply`, applicationDto);
  }

  getStudentApplications(): Observable<JobApplicationResponseDto[]> {
    return this.apiService.get<JobApplicationResponseDto[]>(`${this.studentEndpoint}/jobs/applications`);
  }

  updateJobApplication(applicationId: number, applicationDto: JobApplicationDto): Observable<{ message: string }> {
    return this.apiService.put<{ message: string }>(`${this.studentEndpoint}/jobs/applications/${applicationId}`, applicationDto);
  }
}