export interface Student {
  id: number;
  userId: number;
  studentId: string;
  universityName: string;
  department: string;
  currentSemester: number;
  cgpa: number;
  resumeFilePath?: string;
  isProfileComplete: boolean;
  isVerified: boolean;
  enrollmentDate: Date;
  graduationDate: Date;
}

export interface StudentRegistrationDto {
  studentId: string;
  universityName: string;
  department: string;
  currentSemester: number;
  cgpa: number;
  graduationDate: Date;
  resumeFilePath?: string;
}

export interface StudentProfileDto {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  dateOfBirth: Date;
  address: string;
  studentId: string;
  universityName: string;
  department: string;
  currentSemester: number;
  cgpa: number;
  graduationDate: Date;
  resumeFilePath?: string;
}

export interface StudentResponseDto {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  studentId: string;
  universityName: string;
  department: string;
  currentSemester: number;
  cgpa: number;
  resumeFilePath?: string;
  isProfileComplete: boolean;
  isVerified: boolean;
  enrollmentDate: Date;
  graduationDate: Date;
  enrolledCourses: StudentCourseDto[];
  certificates: CertificateDto[];
}

export interface StudentCourseDto {
  courseId: number;
  courseTitle: string;
  courseCategory: string;
  enrollmentDate: Date;
  completionDate?: Date;
  isCompleted: boolean;
  progressPercentage: number;
  paymentStatus: string;
  amountPaid: number;
}

export interface CertificateDto {
  id: number;
  certificateNumber: string;
  courseName: string;
  issueDate: Date;
  expiryDate?: Date;
  certificateFilePath?: string;
}