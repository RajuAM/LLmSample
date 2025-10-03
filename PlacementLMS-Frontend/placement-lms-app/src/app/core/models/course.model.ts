export interface Course {
  id: number;
  title: string;
  description: string;
  institutionId: number;
  category: string;
  price: number;
  discountPrice: number;
  durationHours: number;
  thumbnailImagePath?: string;
  videoPath?: string;
  pdfMaterialPath?: string;
  isActive: boolean;
  isApproved: boolean;
  createdAt: Date;
  startDate: Date;
  endDate: Date;
}

export interface CourseDto {
  title: string;
  description: string;
  institutionId: number;
  category: string;
  price: number;
  discountPrice: number;
  durationHours: number;
  thumbnailImagePath?: string;
  videoPath?: string;
  pdfMaterialPath?: string;
  startDate: Date;
  endDate: Date;
}

export interface CourseResponseDto {
  id: number;
  title: string;
  description: string;
  institutionName: string;
  category: string;
  price: number;
  discountPrice: number;
  durationHours: number;
  thumbnailImagePath?: string;
  videoPath?: string;
  pdfMaterialPath?: string;
  isActive: boolean;
  createdAt: Date;
  startDate: Date;
  endDate: Date;
  enrollmentCount: number;
  assignments: AssignmentDto[];
}

export interface CourseEnrollmentDto {
  courseId: number;
  amountPaid: number;
  paymentStatus: string;
  paymentTransactionId?: string;
}

export interface Assignment {
  id: number;
  title: string;
  description: string;
  courseId: number;
  dueDate: Date;
  maxPoints: number;
  instructions?: string;
  attachmentPath?: string;
  isActive: boolean;
  createdAt: Date;
}

export interface AssignmentDto {
  title: string;
  description: string;
  courseId: number;
  dueDate: Date;
  maxPoints: number;
  instructions?: string;
  attachmentPath?: string;
}

export interface AssignmentResponseDto {
  id: number;
  title: string;
  description: string;
  courseTitle: string;
  dueDate: Date;
  maxPoints: number;
  instructions?: string;
  attachmentPath?: string;
  isActive: boolean;
  createdAt: Date;
  submissions?: StudentAssignmentDto[];
}

export interface StudentAssignmentDto {
  assignmentId: number;
  submissionText?: string;
  submissionFilePath?: string;
}

export interface StudentAssignmentResponseDto {
  id: number;
  assignmentTitle: string;
  submittedAt: Date;
  submissionFilePath?: string;
  submissionText?: string;
  pointsScored?: number;
  feedback?: string;
  status: string;
  gradedAt?: Date;
}

export interface CourseAnalyticsDto {
  courseId: number;
  courseTitle: string;
  totalEnrollments: number;
  activeStudents: number;
  completedStudents: number;
  completionRate: number;
  averageProgress: number;
  averageAssignmentScore: number;
  weeklyProgress: WeeklyProgressDto[];
}

export interface WeeklyProgressDto {
  week: Date;
  newEnrollments: number;
  completions: number;
  averageProgress: number;
}