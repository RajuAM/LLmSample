import { Component, OnInit } from '@angular/core';
import { FeedbackService } from '../../../../core/services/feedback.service';
import { FeedbackAnalytics } from '../../../../core/models/feedback.model';

@Component({
  selector: 'app-feedback-analytics',
  templateUrl: './feedback-analytics.component.html',
  styleUrls: ['./feedback-analytics.component.css']
})
export class FeedbackAnalyticsComponent implements OnInit {
  analytics: FeedbackAnalytics | null = null;
  loading = false;
  error = '';

  // Chart data
  ratingDistribution: any[] = [];
  categoryDistribution: any[] = [];
  typeDistribution: any[] = [];
  monthlyTrends: any[] = [];

  constructor(private feedbackService: FeedbackService) { }

  ngOnInit(): void {
    this.loadAnalytics();
  }

  loadAnalytics(): void {
    this.loading = true;
    this.error = '';

    this.feedbackService.getFeedbackAnalytics().subscribe({
      next: (data) => {
        this.analytics = data;
        this.prepareChartData();
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load analytics';
        this.loading = false;
        console.error('Error loading analytics:', error);
      }
    });
  }

  prepareChartData(): void {
    if (!this.analytics) return;

    // Rating distribution for pie chart
    this.ratingDistribution = [
      { name: '5 Stars', value: this.analytics.fiveStarCount },
      { name: '4 Stars', value: this.analytics.fourStarCount },
      { name: '3 Stars', value: this.analytics.threeStarCount },
      { name: '2 Stars', value: this.analytics.twoStarCount },
      { name: '1 Star', value: this.analytics.oneStarCount }
    ];

    // Category distribution for bar chart
    this.categoryDistribution = Object.entries(this.analytics.feedbackByCategory).map(([key, value]) => ({
      name: key,
      value: value
    }));

    // Type distribution for bar chart
    this.typeDistribution = Object.entries(this.analytics.feedbackByType).map(([key, value]) => ({
      name: key,
      value: value
    }));

    // Monthly trends for line chart
    this.monthlyTrends = this.analytics.monthlyTrends.map(trend => ({
      name: trend.month,
      value: trend.count
    }));
  }

  getTopRatedStudents(): any[] {
    return this.analytics?.topRatedStudents.slice(0, 5) || [];
  }

  getTopRatedCompanies(): any[] {
    return this.analytics?.topRatedCompanies.slice(0, 5) || [];
  }

  getTopRatedCourses(): any[] {
    return this.analytics?.topRatedCourses.slice(0, 5) || [];
  }

  formatRating(rating: number): string {
    return rating.toFixed(1);
  }

  getRatingStars(rating: number): string {
    return '★'.repeat(Math.round(rating)) + '☆'.repeat(5 - Math.round(rating));
  }

  getRatingColor(rating: number): string {
    if (rating >= 4) return '#28a745'; // Green
    if (rating >= 3) return '#ffc107'; // Yellow
    return '#dc3545'; // Red
  }

  getPercentage(value: number): number {
    if (!this.analytics) return 0;
    const total = this.analytics.totalFeedbackCount;
    return total > 0 ? (value / total) * 100 : 0;
  }

  getBarColor(name: string): string {
    const colors = ['#007bff', '#28a745', '#ffc107', '#fd7e14', '#dc3545'];
    const hash = name.split('').reduce((a, b) => {
      a = ((a << 5) - a) + b.charCodeAt(0);
      return a & a;
    }, 0);
    return colors[Math.abs(hash) % colors.length];
  }

  getRandomColor(): string {
    const colors = ['#007bff', '#28a745', '#ffc107', '#fd7e14', '#dc3545', '#6f42c1', '#e83e8c', '#20c997'];
    return colors[Math.floor(Math.random() * colors.length)];
  }

  getResponseRate(): number {
    // This would be calculated based on actual response rate logic
    return Math.round((this.analytics?.averageRating || 0) * 20);
  }

  getTrendPercentage(value: number): number {
    if (this.monthlyTrends.length === 0) return 0;
    const maxValue = Math.max(...this.monthlyTrends.map(t => t.value));
    return maxValue > 0 ? (value / maxValue) * 100 : 0;
  }

  formatMonth(monthStr: string): string {
    const [year, month] = monthStr.split('-');
    const date = new Date(parseInt(year), parseInt(month) - 1);
    return date.toLocaleDateString('en-US', { month: 'short', year: 'numeric' });
  }
}