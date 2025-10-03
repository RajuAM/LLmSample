export interface JobOpportunity {
  id: number;
  title: string;
  description: string;
  industryId: number;
  companyId: number;
  jobType: string;
  experienceLevel: string;
  location: string;
  salaryMin: number;
  salaryMax: number;
  skillsRequired: string;
  responsibilities?: string;
  benefits?: string;
  numberOfPositions: number;
  numberOfApplications: number;
  applicationDeadline: Date;
  isActive: boolean;
  createdAt: Date;
}

export interface JobOpportunityDto {
  title: string;
  description: string;
  industryId: number;
  jobType: string;
  experienceLevel: string;
  location: string;
  salaryMin: number;
  salaryMax: number;
  skillsRequired: string;
  responsibilities?: string;
  benefits?: string;
  numberOfPositions: number;
  applicationDeadline: Date;
}

export interface JobOpportunityResponseDto {
  id: number;
  title: string;
  description: string;
  companyName: string;
  jobType: string;
  experienceLevel: string;
  location: string;
  salaryMin: number;
  salaryMax: number;
  skillsRequired: string;
  responsibilities?: string;
  benefits?: string;
  numberOfPositions: number;
  numberOfApplications: number;
  applicationDeadline: Date;
  isActive: boolean;
  createdAt: Date;
}

export interface JobApplication {
  id: number;
  studentId: number;
  jobOpportunityId: number;
  appliedAt: Date;
  status: string;
  coverLetter?: string;
  interviewDate?: Date;
  interviewFeedback?: string;
  isSelected: boolean;
  selectionDate?: Date;
}

export interface JobApplicationDto {
  jobOpportunityId: number;
  coverLetter?: string;
  resumeId?: string;
}

export interface JobApplicationResponseDto {
  id: number;
  jobTitle: string;
  companyName: string;
  studentName: string;
  appliedAt: Date;
  status: string;
  coverLetter?: string;
  interviewDate?: Date;
  interviewFeedback?: string;
  isSelected: boolean;
  selectionDate?: Date;
}

export interface Company {
  id: number;
  name: string;
  industry: string;
  description?: string;
  website?: string;
  companySize?: string;
  address: string;
  city?: string;
  state?: string;
  postalCode?: string;
  country?: string;
  contactNumber: string;
  contactEmail: string;
  hrContactName?: string;
  hrContactNumber?: string;
  hrContactEmail?: string;
  logoPath?: string;
  isVerified: boolean;
  isActive: boolean;
  registrationDate: Date;
  approvalDate?: Date;
}

export interface CompanyRegistrationDto {
  name: string;
  industry: string;
  description?: string;
  website?: string;
  companySize?: string;
  address: string;
  city?: string;
  state?: string;
  postalCode?: string;
  country?: string;
  contactNumber: string;
  contactEmail: string;
  hrContactName?: string;
  hrContactNumber?: string;
  hrContactEmail?: string;
}

export interface CompanyUpdateDto {
  name?: string;
  description?: string;
  website?: string;
  companySize?: string;
  address?: string;
  city?: string;
  state?: string;
  postalCode?: string;
  country?: string;
  contactNumber?: string;
  contactEmail?: string;
  hrContactName?: string;
  hrContactNumber?: string;
  hrContactEmail?: string;
}

export interface ApplicationStatusDto {
  status: string;
  feedback?: string;
}

export interface InterviewScheduleDto {
  applicationId: number;
  interviewDate: Date;
  location: string;
  interviewerName?: string;
  notes?: string;
}

export interface PlacementStatsDto {
  totalJobOpportunities: number;
  totalApplications: number;
  totalInterviews: number;
  totalSelections: number;
  placementRate: number;
}