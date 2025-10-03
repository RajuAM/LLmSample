import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DashboardService } from '../../../../core/services/dashboard.service';
import {
  ReportFiltersDto,
  StudentReportDto,
  CourseReportDto,
  PlacementReportDto,
  CertificateReportDto,
  ProgressReportDto
} from '../../../../core/models/dashboard.model';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  filterForm!: FormGroup;
  loading = false;
  error = '';

  // Report data
  currentReportType: string = 'students';
  reportData: any = null;

  // Filter options
  departments: string[] = [];
  semesters: number[] = [];
  courseCategories: string[] = [];
  industries: string[] = [];

  // Export options
  exportFormats: string[] = [];
  showExportOptions = false;

  constructor(
    private fb: FormBuilder,
    private dashboardService: DashboardService
  ) {
    this.initializeForm();
  }

  ngOnInit(): void {
    this.loadFilterOptions();
    this.loadReport();
  }

  initializeForm(): void {
    this.filterForm = this.fb.group({
      startDate: [''],
      endDate: [''],
      department: [''],
      semester: [''],
      courseCategory: [''],
      industry: ['']
    });
  }

  loadFilterOptions(): void {
    this.departments = this.dashboardService.getDepartmentList();
    this.semesters = this.dashboardService.getSemesterList();
    this.courseCategories = this.dashboardService.getCourseCategories();
    this.industries = this.dashboardService.getIndustries();
    this.exportFormats = this.dashboardService.getExportFormats();
  }

  loadReport(): void {
    this.loading = true;
    this.error = '';

    const filters: ReportFiltersDto = this.filterForm.value;

    switch (this.currentReportType) {
      case 'students':
        this.loadStudentReport(filters);
        break;
      case 'courses':
        this.loadCourseReport(filters);
        break;
      case 'placements':
        this.loadPlacementReport(filters);
        break;
      case 'certificates':
        this.loadCertificateReport(filters);
        break;
      case 'progress':
        this.loadProgressReport(filters);
        break;
    }
  }

  loadStudentReport(filters: ReportFiltersDto): void {
    this.dashboardService.getStudentReport(filters).subscribe({
      next: (data) => {
        this.reportData = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load student report';
        this.loading = false;
        console.error('Error loading student report:', error);
      }
    });
  }

  loadCourseReport(filters: ReportFiltersDto): void {
    this.dashboardService.getCourseReport(filters).subscribe({
      next: (data) => {
        this.reportData = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load course report';
        this.loading = false;
        console.error('Error loading course report:', error);
      }
    });
  }

  loadPlacementReport(filters: ReportFiltersDto): void {
    this.dashboardService.getPlacementReport(filters).subscribe({
      next: (data) => {
        this.reportData = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load placement report';
        this.loading = false;
        console.error('Error loading placement report:', error);
      }
    });
  }

  loadCertificateReport(filters: ReportFiltersDto): void {
    this.dashboardService.getCertificateReport(filters).subscribe({
      next: (data) => {
        this.reportData = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load certificate report';
        this.loading = false;
        console.error('Error loading certificate report:', error);
      }
    });
  }

  loadProgressReport(filters: ReportFiltersDto): void {
    this.dashboardService.getProgressReport(filters).subscribe({
      next: (data) => {
        this.reportData = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load progress report';
        this.loading = false;
        console.error('Error loading progress report:', error);
      }
    });
  }

  onReportTypeChange(reportType: string): void {
    this.currentReportType = reportType;
    this.loadReport();
  }

  onFiltersChange(): void {
    this.loadReport();
  }

  clearFilters(): void {
    this.filterForm.reset();
    this.loadReport();
  }

  exportReport(format: string): void {
    const filters: ReportFiltersDto = this.filterForm.value;

    switch (this.currentReportType) {
      case 'students':
        this.dashboardService.exportStudentReport(filters, format).subscribe({
          next: (blob) => {
            this.downloadFile(blob, `StudentReport_${new Date().toISOString().split('T')[0]}.${format}`);
          },
          error: (error) => {
            console.error('Error exporting student report:', error);
          }
        });
        break;
      case 'courses':
        this.dashboardService.exportCourseReport(filters, format).subscribe({
          next: (blob) => {
            this.downloadFile(blob, `CourseReport_${new Date().toISOString().split('T')[0]}.${format}`);
          },
          error: (error) => {
            console.error('Error exporting course report:', error);
          }
        });
        break;
      case 'placements':
        this.dashboardService.exportPlacementReport(filters, format).subscribe({
          next: (blob) => {
            this.downloadFile(blob, `PlacementReport_${new Date().toISOString().split('T')[0]}.${format}`);
          },
          error: (error) => {
            console.error('Error exporting placement report:', error);
          }
        });
        break;
      case 'certificates':
        this.dashboardService.exportCertificateReport(filters, format).subscribe({
          next: (blob) => {
            this.downloadFile(blob, `CertificateReport_${new Date().toISOString().split('T')[0]}.${format}`);
          },
          error: (error) => {
            console.error('Error exporting certificate report:', error);
          }
        });
        break;
    }
  }

  private downloadFile(blob: any, filename: string): void {
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(url);
    document.body.removeChild(a);
  }

  toggleExportOptions(): void {
    this.showExportOptions = !this.showExportOptions;
  }

  // Utility methods for template
  formatNumber(num: number): string {
    return num?.toLocaleString() || '0';
  }

  formatPercentage(num: number): string {
    return `${num?.toFixed(1) || '0'}%`;
  }

  formatCurrency(num: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
      maximumFractionDigits: 0
    }).format(num || 0);
  }

  formatDate(date: string | Date): string {
    return new Date(date).toLocaleDateString();
  }

  getStatusColor(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active': return '#28a745';
      case 'completed': return '#007bff';
      case 'pending': return '#ffc107';
      case 'inactive': return '#6c757d';
      default: return '#6c757d';
    }
  }

  getGradeColor(grade: string): string {
    switch (grade?.toUpperCase()) {
      case 'A': return '#28a745';
      case 'B': return '#007bff';
      case 'C': return '#ffc107';
      case 'D': return '#fd7e14';
      case 'F': return '#dc3545';
      default: return '#6c757d';
    }
  }
}