import { Component, OnInit } from '@angular/core';
import { PlacementService } from '../../../../core/services/placement.service';
import { JobOpportunityResponseDto, JobApplicationDto } from '../../../../core/models/placement.model';

@Component({
  selector: 'app-job-opportunities',
  templateUrl: './job-opportunities.component.html',
  styleUrls: ['./job-opportunities.component.css']
})
export class JobOpportunitiesComponent implements OnInit {
  jobOpportunities: JobOpportunityResponseDto[] = [];
  filteredJobs: JobOpportunityResponseDto[] = [];
  jobTypes: string[] = ['Full-time', 'Part-time', 'Internship', 'All'];
  experienceLevels: string[] = ['Entry', 'Mid', 'Senior', 'All'];
  selectedJobType: string = 'All';
  selectedExperienceLevel: string = 'All';
  isLoading = false;
  searchTerm = '';

  constructor(private placementService: PlacementService) { }

  ngOnInit(): void {
    this.loadJobOpportunities();
  }

  loadJobOpportunities(): void {
    this.isLoading = true;
    this.placementService.getAvailableJobOpportunities().subscribe({
      next: (jobs) => {
        this.jobOpportunities = jobs;
        this.filteredJobs = jobs;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading job opportunities:', error);
        this.isLoading = false;
      }
    });
  }

  filterByJobType(jobType: string): void {
    this.selectedJobType = jobType;
    this.applyFilters();
  }

  filterByExperienceLevel(level: string): void {
    this.selectedExperienceLevel = level;
    this.applyFilters();
  }

  onSearch(): void {
    this.applyFilters();
  }

  private applyFilters(): void {
    let filtered = this.jobOpportunities;

    // Filter by job type
    if (this.selectedJobType !== 'All') {
      filtered = filtered.filter(job => job.jobType === this.selectedJobType);
    }

    // Filter by experience level
    if (this.selectedExperienceLevel !== 'All') {
      filtered = filtered.filter(job => job.experienceLevel === this.selectedExperienceLevel);
    }

    // Filter by search term
    if (this.searchTerm.trim()) {
      const searchLower = this.searchTerm.toLowerCase();
      filtered = filtered.filter(job =>
        job.title.toLowerCase().includes(searchLower) ||
        job.description.toLowerCase().includes(searchLower) ||
        job.companyName.toLowerCase().includes(searchLower) ||
        job.skillsRequired.toLowerCase().includes(searchLower)
      );
    }

    this.filteredJobs = filtered;
  }

  applyForJob(jobId: number): void {
    const applicationDto: JobApplicationDto = {
      jobOpportunityId: jobId,
      coverLetter: `I am interested in the ${this.filteredJobs.find(j => j.id === jobId)?.title} position at ${this.filteredJobs.find(j => j.id === jobId)?.companyName}.`
    };

    this.placementService.applyForJob(applicationDto).subscribe({
      next: (response) => {
        alert('Application submitted successfully!');
        this.loadJobOpportunities(); // Refresh the list
      },
      error: (error) => {
        console.error('Error applying for job:', error);
        alert('Error submitting application. Please try again.');
      }
    });
  }

  viewJobDetails(jobId: number): void {
    // Implementation for viewing job details
    console.log('View job details:', jobId);
  }

  formatSalary(salaryMin: number, salaryMax: number): string {
    const formatAmount = (amount: number) => {
      if (amount >= 100000) {
        return `₹${(amount / 100000).toFixed(1)}L`;
      }
      return `₹${amount.toLocaleString()}`;
    };

    if (salaryMin === salaryMax) {
      return formatAmount(salaryMin);
    }
    return `${formatAmount(salaryMin)} - ${formatAmount(salaryMax)}`;
  }

  getDaysUntilDeadline(deadline: Date): number {
    const today = new Date();
    const timeDiff = deadline.getTime() - today.getTime();
    return Math.ceil(timeDiff / (1000 * 3600 * 24));
  }

  isDeadlineApproaching(deadline: Date): boolean {
    return this.getDaysUntilDeadline(deadline) <= 7;
  }
}