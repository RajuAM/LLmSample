import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { ApiService } from './api.service';
import {
  Feedback,
  CreateFeedbackDto,
  UpdateFeedbackDto,
  FeedbackAnalytics,
  Assessment,
  CreateAssessmentDto,
  StudentAssessment,
  AssessmentAnalytics,
  FeedbackFilters,
  AssessmentFilters
} from '../models/feedback.model';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  private readonly feedbackEndpoint = '/feedback';
  private readonly assessmentEndpoint = '/assessment';

  constructor(private apiService: ApiService) { }

  // Feedback Management
  createFeedback(feedbackDto: CreateFeedbackDto): Observable<Feedback> {
    return this.apiService.post<Feedback>(this.feedbackEndpoint, feedbackDto);
  }

  getFeedbackById(feedbackId: number): Observable<Feedback> {
    return this.apiService.get<Feedback>(`${this.feedbackEndpoint}/${feedbackId}`);
  }

  getFeedbackForUser(feedbackType?: string): Observable<Feedback[]> {
    const query = feedbackType ? `?feedbackType=${feedbackType}` : '';
    return this.apiService.get<Feedback[]>(`${this.feedbackEndpoint}/received${query}`);
  }

  getFeedbackByUser(feedbackType?: string): Observable<Feedback[]> {
    const query = feedbackType ? `?feedbackType=${feedbackType}` : '';
    return this.apiService.get<Feedback[]>(`${this.feedbackEndpoint}/given${query}`);
  }

  getFeedbackForStudent(studentId: number): Observable<Feedback[]> {
    return this.apiService.get<Feedback[]>(`${this.feedbackEndpoint}/student/${studentId}`);
  }

  getFeedbackForCompany(companyId: number): Observable<Feedback[]> {
    return this.apiService.get<Feedback[]>(`${this.feedbackEndpoint}/company/${companyId}`);
  }

  getFeedbackForCourse(courseId: number): Observable<Feedback[]> {
    return this.apiService.get<Feedback[]>(`${this.feedbackEndpoint}/course/${courseId}`);
  }

  updateFeedback(feedbackId: number, feedbackDto: UpdateFeedbackDto): Observable<void> {
    return this.apiService.put<void>(`${this.feedbackEndpoint}/${feedbackId}`, feedbackDto);
  }

  deleteFeedback(feedbackId: number): Observable<void> {
    return this.apiService.delete<void>(`${this.feedbackEndpoint}/${feedbackId}`);
  }

  getRecentFeedback(count: number = 10): Observable<Feedback[]> {
    return this.apiService.get<Feedback[]>(`${this.feedbackEndpoint}/recent?count=${count}`);
  }

  // Analytics
  getFeedbackAnalytics(userId?: number, companyId?: number, courseId?: number): Observable<FeedbackAnalytics> {
    let query = '';
    const params = [];
    if (userId) params.push(`userId=${userId}`);
    if (companyId) params.push(`companyId=${companyId}`);
    if (courseId) params.push(`courseId=${courseId}`);
    if (params.length > 0) query = `?${params.join('&')}`;

    return this.apiService.get<FeedbackAnalytics>(`${this.feedbackEndpoint}/analytics${query}`);
  }

  getAverageRating(entityType: string, entityId: number): Observable<{ averageRating: number }> {
    return this.apiService.get<{ averageRating: number }>(`${this.feedbackEndpoint}/rating/${entityType}/${entityId}`);
  }

  // Assessment Management
  createAssessment(assessmentDto: CreateAssessmentDto): Observable<Assessment> {
    return this.apiService.post<Assessment>(this.assessmentEndpoint, assessmentDto);
  }

  getAssessmentById(assessmentId: number): Observable<Assessment> {
    return this.apiService.get<Assessment>(`${this.assessmentEndpoint}/${assessmentId}`);
  }

  getAssessmentsForUser(): Observable<Assessment[]> {
    return this.apiService.get<Assessment[]>(`${this.assessmentEndpoint}/user`);
  }

  getAssessmentsByType(assessmentType: string): Observable<Assessment[]> {
    return this.apiService.get<Assessment[]>(`${this.assessmentEndpoint}/type/${assessmentType}`);
  }

  getActiveAssessments(): Observable<Assessment[]> {
    return this.apiService.get<Assessment[]>(`${this.assessmentEndpoint}/active`);
  }

  updateAssessment(assessmentId: number, assessmentDto: CreateAssessmentDto): Observable<void> {
    return this.apiService.put<void>(`${this.assessmentEndpoint}/${assessmentId}`, assessmentDto);
  }

  deleteAssessment(assessmentId: number): Observable<void> {
    return this.apiService.delete<void>(`${this.assessmentEndpoint}/${assessmentId}`);
  }

  assignAssessmentToStudent(assessmentId: number, studentId: number): Observable<Assessment> {
    return this.apiService.post<Assessment>(`${this.assessmentEndpoint}/${assessmentId}/assign/${studentId}`, {});
  }

  startAssessment(studentAssessmentId: number): Observable<StudentAssessment> {
    return this.apiService.post<StudentAssessment>(`${this.assessmentEndpoint}/start/${studentAssessmentId}`, {});
  }

  submitAssessment(studentAssessmentId: number, answers: { [questionId: number]: string }): Observable<StudentAssessment> {
    return this.apiService.post<StudentAssessment>(`${this.assessmentEndpoint}/submit/${studentAssessmentId}`, answers);
  }

  getStudentAssessment(studentId: number, assessmentId: number): Observable<StudentAssessment> {
    return this.apiService.get<StudentAssessment>(`${this.assessmentEndpoint}/student/${studentId}/assessment/${assessmentId}`);
  }

  getAssessmentResults(assessmentId: number): Observable<StudentAssessment[]> {
    return this.apiService.get<StudentAssessment[]>(`${this.assessmentEndpoint}/${assessmentId}/results`);
  }

  getAssessmentAnalytics(assessmentId: number): Observable<AssessmentAnalytics> {
    return this.apiService.get<AssessmentAnalytics>(`${this.assessmentEndpoint}/${assessmentId}/analytics`);
  }

  // Utility methods
  getFeedbackTypes(): string[] {
    return [
      'RecruiterToStudent',
      'StudentToProcess',
      'ContentFeedback',
      'TrainingFeedback',
      'CourseFeedback',
      'InterviewFeedback'
    ];
  }

  getFeedbackCategories(): string[] {
    return [
      'Technical',
      'Communication',
      'Process',
      'Content',
      'Overall',
      'Leadership',
      'Teamwork',
      'ProblemSolving'
    ];
  }

  getAssessmentTypes(): string[] {
    return [
      'Technical',
      'Behavioral',
      'Communication',
      'Leadership',
      'DomainKnowledge',
      'SoftSkills',
      'Aptitude'
    ];
  }

  getTargetAudiences(): string[] {
    return [
      'Students',
      'Recruiters',
      'Instructors',
      'Companies',
      'All'
    ];
  }

  getQuestionTypes(): string[] {
    return [
      'MultipleChoice',
      'Rating',
      'Text',
      'YesNo',
      'Scale'
    ];
  }
}