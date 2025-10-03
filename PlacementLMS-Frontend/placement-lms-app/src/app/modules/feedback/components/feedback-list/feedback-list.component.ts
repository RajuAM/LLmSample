import { Component, OnInit } from '@angular/core';
import { FeedbackService } from '../../../../core/services/feedback.service';
import { Feedback, FeedbackFilters } from '../../../../core/models/feedback.model';

@Component({
  selector: 'app-feedback-list',
  templateUrl: './feedback-list.component.html',
  styleUrls: ['./feedback-list.component.css']
})
export class FeedbackListComponent implements OnInit {
  feedbacks: Feedback[] = [];
  filteredFeedbacks: Feedback[] = [];
  loading = false;
  error = '';

  // Filter properties
  filters: FeedbackFilters = {};
  feedbackTypes: string[] = [];
  categories: string[] = [];

  constructor(private feedbackService: FeedbackService) { }

  ngOnInit(): void {
    this.loadFeedbacks();
    this.initializeFilterData();
  }

  loadFeedbacks(): void {
    this.loading = true;
    this.error = '';

    this.feedbackService.getFeedbackForUser().subscribe({
      next: (feedbacks) => {
        this.feedbacks = feedbacks;
        this.applyFilters();
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load feedback';
        this.loading = false;
        console.error('Error loading feedback:', error);
      }
    });
  }

  initializeFilterData(): void {
    this.feedbackTypes = this.feedbackService.getFeedbackTypes();
    this.categories = this.feedbackService.getFeedbackCategories();
  }

  applyFilters(): void {
    this.filteredFeedbacks = this.feedbacks.filter(feedback => {
      if (this.filters.feedbackType && feedback.feedbackType !== this.filters.feedbackType) {
        return false;
      }
      if (this.filters.category && feedback.category !== this.filters.category) {
        return false;
      }
      if (this.filters.rating && feedback.rating !== this.filters.rating) {
        return false;
      }
      if (this.filters.dateFrom && new Date(feedback.createdAt) < this.filters.dateFrom) {
        return false;
      }
      if (this.filters.dateTo && new Date(feedback.createdAt) > this.filters.dateTo) {
        return false;
      }
      return true;
    });
  }

  onFilterChange(): void {
    this.applyFilters();
  }

  clearFilters(): void {
    this.filters = {};
    this.applyFilters();
  }

  deleteFeedback(feedbackId: number): void {
    if (confirm('Are you sure you want to delete this feedback?')) {
      this.feedbackService.deleteFeedback(feedbackId).subscribe({
        next: () => {
          this.loadFeedbacks();
        },
        error: (error) => {
          console.error('Error deleting feedback:', error);
        }
      });
    }
  }

  getRatingStars(rating: number): string {
    return '★'.repeat(rating) + '☆'.repeat(5 - rating);
  }

  getRatingColor(rating: number): string {
    if (rating >= 4) return '#28a745'; // Green
    if (rating >= 3) return '#ffc107'; // Yellow
    return '#dc3545'; // Red
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }

  editFeedback(feedbackId: number): void {
    // Navigate to edit form - this would typically use Angular Router
    console.log('Edit feedback:', feedbackId);
  }
}