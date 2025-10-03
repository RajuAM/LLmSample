import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval, Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { DashboardService } from '../../../../core/services/dashboard.service';
import {
  DashboardDto,
  DashboardSummaryDto,
  ChartData,
  TrendData,
  BreakdownData
} from '../../../../core/models/dashboard.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, OnDestroy {
  dashboardData: DashboardDto | null = null;
  summary: DashboardSummaryDto | null = null;
  loading = false;
  error = '';
  realTimeUpdate = true;

  // Chart data
  registrationTrendData: ChartData | null = null;
  enrollmentTrendData: ChartData | null = null;
  placementTrendData: ChartData | null = null;
  departmentBreakdownData: ChartData | null = null;
  courseCategoryData: ChartData | null = null;

  private updateSubscription: Subscription | null = null;

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.loadDashboardData();

    // Set up real-time updates every 30 seconds
    if (this.realTimeUpdate) {
      this.updateSubscription = interval(30000).pipe(
        switchMap(() => this.dashboardService.getRealTimeSummary())
      ).subscribe({
        next: (summary) => {
          if (this.summary) {
            this.summary = { ...this.summary, ...summary };
          }
        },
        error: (error) => {
          console.error('Real-time update error:', error);
        }
      });
    }
  }

  ngOnDestroy(): void {
    if (this.updateSubscription) {
      this.updateSubscription.unsubscribe();
    }
  }

  loadDashboardData(): void {
    this.loading = true;
    this.error = '';

    this.dashboardService.getDashboardData().subscribe({
      next: (data) => {
        this.dashboardData = data;
        this.summary = data.summary;
        this.prepareChartData();
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load dashboard data';
        this.loading = false;
        console.error('Error loading dashboard:', error);
      }
    });
  }

  prepareChartData(): void {
    if (!this.dashboardData) return;

    // Registration trends chart
    this.registrationTrendData = this.prepareTrendChart(
      this.dashboardData.studentAnalytics.registrationTrends.map(t => ({ month: t.month, value: t.count })),
      'Student Registrations',
      '#007bff'
    );

    // Enrollment trends chart
    this.enrollmentTrendData = this.prepareTrendChart(
      this.dashboardData.courseAnalytics.enrollmentTrends.map(t => ({ month: t.month, value: t.enrollmentCount })),
      'Course Enrollments',
      '#28a745'
    );

    // Placement trends chart
    this.placementTrendData = this.prepareTrendChart(
      this.dashboardData.placementAnalytics.placementTrends.map(t => ({ month: t.month, value: t.placementCount })),
      'Placements',
      '#ffc107'
    );

    // Department breakdown chart
    this.departmentBreakdownData = this.prepareBreakdownChart(
      this.dashboardData.studentAnalytics.departmentBreakdown.map(d => ({
        category: d.department,
        count: d.studentCount,
        percentage: d.percentage
      })),
      'Students by Department'
    );

    // Course category breakdown chart
    this.courseCategoryData = this.prepareBreakdownChart(
      this.dashboardData.courseAnalytics.categoryBreakdown.map(c => ({
        category: c.category,
        count: c.count,
        percentage: c.percentage
      })),
      'Courses by Category'
    );
  }

  private prepareTrendChart(trends: TrendData[], label: string, color: string): ChartData {
    return {
      labels: trends.map(t => this.formatMonth(t.month)),
      datasets: [{
        label: label,
        data: trends.map(t => t.value),
        backgroundColor: color,
        borderColor: color,
        borderWidth: 2
      }]
    };
  }

  private prepareBreakdownChart(breakdown: BreakdownData[], label: string): ChartData {
    const colors = ['#007bff', '#28a745', '#ffc107', '#fd7e14', '#dc3545', '#6f42c1', '#e83e8c', '#20c997'];

    return {
      labels: breakdown.map(b => b.category),
      datasets: [{
        label: label,
        data: breakdown.map(b => b.count),
        backgroundColor: colors.slice(0, breakdown.length),
        borderWidth: 1
      }]
    };
  }

  private formatMonth(monthStr: string): string {
    const [year, month] = monthStr.split('-');
    const date = new Date(parseInt(year), parseInt(month) - 1);
    return date.toLocaleDateString('en-US', { month: 'short', year: 'numeric' });
  }

  // Chart options
  getChartOptions(title: string) {
    return {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        title: {
          display: true,
          text: title,
          font: {
            size: 16
          }
        },
        legend: {
          display: true,
          position: 'bottom' as const
        }
      },
      scales: {
        y: {
          beginAtZero: true
        }
      }
    };
  }

  getPieChartOptions(title: string) {
    return {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        title: {
          display: true,
          text: title,
          font: {
            size: 16
          }
        },
        legend: {
          display: true,
          position: 'right' as const
        }
      }
    };
  }

  refreshData(): void {
    this.loadDashboardData();
  }

  toggleRealTimeUpdates(): void {
    this.realTimeUpdate = !this.realTimeUpdate;

    if (this.realTimeUpdate) {
      this.ngOnInit(); // Restart real-time updates
    } else {
      if (this.updateSubscription) {
        this.updateSubscription.unsubscribe();
        this.updateSubscription = null;
      }
    }
  }

  // Utility methods for template
  formatNumber(num: number): string {
    return num.toLocaleString();
  }

  formatPercentage(num: number): string {
    return `${num.toFixed(1)}%`;
  }

  formatCurrency(num: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
      maximumFractionDigits: 0
    }).format(num);
  }

  getStatusColor(percentage: number): string {
    if (percentage >= 80) return '#28a745'; // Green
    if (percentage >= 60) return '#ffc107'; // Yellow
    if (percentage >= 40) return '#fd7e14'; // Orange
    return '#dc3545'; // Red
  }

  getPlacementRateColor(): string {
    return this.getStatusColor(this.summary?.placementRate || 0);
  }

  getCompletionRateColor(): string {
    return this.getStatusColor(this.dashboardData?.courseAnalytics.averageCompletionRate || 0);
  }

  getRatingStars(rating: number): number[] {
    return Array(Math.round(rating)).fill(0);
  }
}