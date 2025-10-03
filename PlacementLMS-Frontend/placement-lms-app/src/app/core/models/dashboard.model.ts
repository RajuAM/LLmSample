// Dashboard Analytics Models
export interface DashboardDto {
  summary: DashboardSummaryDto;
  studentAnalytics: StudentAnalyticsDto;
  courseAnalytics: CourseAnalyticsDto;
  placementAnalytics: PlacementAnalyticsDto;
  certificateAnalytics: CertificateAnalyticsDto;
}

export interface DashboardSummaryDto {
  totalStudents: number;
  totalCompanies: number;
  totalCourses: number;
  totalJobOpportunities: number;
  totalPlacements: number;
  totalCertificates: number;
  placementRate: number;
  averageCourseRating: number;
}

export interface StudentAnalyticsDto {
  newRegistrationsThisMonth: number;
  activeStudents: number;
  averageProgress: number;
  registrationTrends: MonthlyRegistrationDto[];
  departmentBreakdown: DepartmentBreakdownDto[];
  semesterBreakdown: SemesterBreakdownDto[];
}

export interface CourseAnalyticsDto {
  totalCourses: number;
  totalEnrollments: number;
  averageCompletionRate: number;
  topCourses: CourseSubscriptionDto[];
  categoryBreakdown: CategoryBreakdownDto[];
  enrollmentTrends: MonthlyEnrollmentDto[];
}

export interface PlacementAnalyticsDto {
  totalPlacements: number;
  totalApplications: number;
  totalJobOpportunities: number;
  placementRate: number;
  averageSalary: number;
  topIndustries: IndustryPlacementDto[];
  placementTrends: MonthlyPlacementDto[];
  topCompanies: CompanyPlacementDto[];
}

export interface CertificateAnalyticsDto {
  totalCertificates: number;
  certificatesThisMonth: number;
  averageGrade: number;
  courseBreakdown: CertificateBreakdownDto[];
  recentCertificates: RecentCertificateDto[];
}

// Supporting Models
export interface MonthlyRegistrationDto {
  month: string;
  count: number;
  cumulativeCount: number;
}

export interface DepartmentBreakdownDto {
  department: string;
  studentCount: number;
  percentage: number;
}

export interface SemesterBreakdownDto {
  semester: number;
  studentCount: number;
  percentage: number;
}

export interface CourseSubscriptionDto {
  courseId: number;
  courseTitle: string;
  enrollmentCount: number;
  averageRating: number;
  completionRate: number;
}

export interface CategoryBreakdownDto {
  category: string;
  count: number;
  percentage: number;
}

export interface MonthlyEnrollmentDto {
  month: string;
  enrollmentCount: number;
}

export interface IndustryPlacementDto {
  industry: string;
  placementCount: number;
  averageSalary: number;
  topCompanies: string[];
}

export interface MonthlyPlacementDto {
  month: string;
  placementCount: number;
  successRate: number;
}

export interface CompanyPlacementDto {
  companyName: string;
  placementCount: number;
  averageSalary: number;
}

export interface CertificateBreakdownDto {
  courseName: string;
  certificateCount: number;
  averageGrade: number;
}

export interface RecentCertificateDto {
  id: number;
  studentName: string;
  courseName: string;
  grade: string;
  issueDate: Date;
}

// Report Models
export interface ReportDto {
  reportType: string;
  generatedAt: Date;
  filters: ReportFiltersDto;
  data: any;
}

export interface ReportFiltersDto {
  startDate?: Date;
  endDate?: Date;
  department?: string;
  courseCategory?: string;
  industry?: string;
  semester?: number;
}

export interface StudentReportDto {
  students: StudentDetailDto[];
  totalCount: number;
  statusBreakdown: { [key: string]: number };
  departmentBreakdown: { [key: string]: number };
}

export interface StudentDetailDto {
  id: number;
  studentId: string;
  studentName: string;
  department: string;
  semester: number;
  cgpa: number;
  completedCourses: number;
  totalCourses: number;
  certificatesEarned: number;
  hasPlacement: boolean;
  enrollmentDate: Date;
  status: string;
}

export interface CourseReportDto {
  courses: CourseDetailDto[];
  totalCourses: number;
  categoryBreakdown: { [key: string]: number };
  institutionBreakdown: { [key: string]: number };
}

export interface CourseDetailDto {
  id: number;
  title: string;
  category: string;
  institutionName: string;
  enrollmentCount: number;
  completionCount: number;
  completionRate: number;
  averageRating: number;
  price: number;
  durationHours: number;
  createdAt: Date;
}

export interface PlacementReportDto {
  placements: PlacementDetailDto[];
  totalPlacements: number;
  industryBreakdown: { [key: string]: number };
  companyBreakdown: { [key: string]: number };
  salaryAnalytics: SalaryAnalyticsDto;
}

export interface PlacementDetailDto {
  id: number;
  studentName: string;
  companyName: string;
  jobTitle: string;
  industry: string;
  salary: number;
  placementDate: Date;
  jobType: string;
  location: string;
}

export interface SalaryAnalyticsDto {
  averageSalary: number;
  medianSalary: number;
  highestSalary: number;
  lowestSalary: number;
  salaryByIndustry: { [key: string]: number };
  salaryByExperience: { [key: string]: number };
}

export interface CertificateReportDto {
  certificates: CertificateDetailDto[];
  totalCertificates: number;
  courseBreakdown: { [key: string]: number };
  gradeBreakdown: { [key: string]: number };
}

export interface CertificateDetailDto {
  id: number;
  certificateTitle: string;
  studentName: string;
  courseName: string;
  institutionName: string;
  grade: string;
  issueDate: Date;
  expiryDate?: Date;
}

export interface ProgressReportDto {
  studentProgress: StudentProgressDetailDto[];
  testScores: TestScoreReportDto;
  assignments: AssignmentReportDto;
}

export interface StudentProgressDetailDto {
  studentId: number;
  studentName: string;
  totalCoursesEnrolled: number;
  completedCourses: number;
  inProgressCourses: number;
  averageScore: number;
  totalAssignments: number;
  completedAssignments: number;
  totalTests: number;
  passedTests: number;
  lastActivity: Date;
}

export interface TestScoreReportDto {
  tests: TestDetailDto[];
  overallAverageScore: number;
  totalTests: number;
  passedTests: number;
  failedTests: number;
  passRate: number;
}

export interface TestDetailDto {
  id: number;
  testName: string;
  courseName: string;
  studentName: string;
  score: number;
  maxScore: number;
  grade: string;
  attemptDate: Date;
  timeTakenMinutes: number;
}

export interface AssignmentReportDto {
  assignments: AssignmentDetailDto[];
  totalAssignments: number;
  submittedAssignments: number;
  gradedAssignments: number;
  averageGrade: number;
}

export interface AssignmentDetailDto {
  id: number;
  assignmentTitle: string;
  courseName: string;
  studentName: string;
  dueDate: Date;
  submissionDate?: Date;
  status: string;
  score?: number;
  grade: string;
}

// Chart Data Models for Visualization
export interface ChartData {
  labels: string[];
  datasets: ChartDataset[];
}

export interface ChartDataset {
  label: string;
  data: number[];
  backgroundColor?: string | string[];
  borderColor?: string | string[];
  borderWidth?: number;
}

export interface TrendData {
  month: string;
  value: number;
  label?: string;
}

export interface BreakdownData {
  category: string;
  count: number;
  percentage: number;
  color?: string;
}

// Filter Models
export interface DashboardFilters {
  dateRange?: {
    startDate: Date;
    endDate: Date;
  };
  departments?: string[];
  semesters?: number[];
  courseCategories?: string[];
  industries?: string[];
  companies?: string[];
}

export interface ExportOptions {
  format: 'excel' | 'pdf' | 'csv';
  includeCharts: boolean;
  sections: string[];
}