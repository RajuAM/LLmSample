// Feedback Models
export interface Feedback {
  id: number;
  fromUserId: number;
  fromUserName: string;
  toUserId: number;
  toUserName: string;
  studentId?: number;
  studentName?: string;
  companyId?: number;
  companyName?: string;
  courseId?: number;
  courseTitle?: string;
  jobApplicationId?: number;
  trainingSessionId?: number;
  feedbackType: string;
  category: string;
  rating: number;
  comments: string;
  strengths?: string;
  areasForImprovement?: string;
  isAnonymous: boolean;
  isPublished: boolean;
  createdAt: Date;
  updatedAt: Date;
}

export interface CreateFeedbackDto {
  toUserId: number;
  studentId?: number;
  companyId?: number;
  courseId?: number;
  jobApplicationId?: number;
  trainingSessionId?: number;
  feedbackType: string;
  category: string;
  rating: number;
  comments: string;
  strengths?: string;
  areasForImprovement?: string;
  isAnonymous: boolean;
}

export interface UpdateFeedbackDto {
  category: string;
  rating: number;
  comments: string;
  strengths?: string;
  areasForImprovement?: string;
  isPublished: boolean;
}

// Assessment Models
export interface Assessment {
  id: number;
  title: string;
  description: string;
  createdByUserId: number;
  assessmentType: string;
  targetAudience: string;
  totalPoints: number;
  timeLimitHours?: number;
  startDate?: Date;
  endDate?: Date;
  isActive: boolean;
  isTemplate: boolean;
  createdAt: Date;
  updatedAt: Date;
  questions?: AssessmentQuestion[];
  studentAssessments?: StudentAssessment[];
}

export interface AssessmentQuestion {
  id: number;
  assessmentId: number;
  questionText: string;
  questionType: string;
  points: number;
  order: number;
  isRequired: boolean;
  options?: string;
  correctAnswer?: string;
  createdAt: Date;
  studentAnswers?: StudentAssessmentAnswer[];
}

export interface StudentAssessment {
  id: number;
  studentId: number;
  assessmentId: number;
  startedAt?: Date;
  completedAt?: Date;
  score: number;
  percentage: number;
  isCompleted: boolean;
  isPassed: boolean;
  status: string;
  createdAt: Date;
  updatedAt: Date;
  student?: any;
  assessment?: Assessment;
  answers?: StudentAssessmentAnswer[];
}

export interface StudentAssessmentAnswer {
  id: number;
  studentAssessmentId: number;
  questionId: number;
  answerText?: string;
  rating?: number;
  yesNoAnswer?: boolean;
  pointsEarned: number;
  isCorrect: boolean;
  answeredAt: Date;
  studentAssessment?: StudentAssessment;
  question?: AssessmentQuestion;
}

export interface CreateAssessmentDto {
  title: string;
  description: string;
  assessmentType: string;
  targetAudience: string;
  totalPoints: number;
  timeLimitHours?: number;
  startDate?: Date;
  endDate?: Date;
  questions: CreateAssessmentQuestionDto[];
}

export interface CreateAssessmentQuestionDto {
  questionText: string;
  questionType: string;
  points: number;
  order: number;
  isRequired: boolean;
  options?: string;
  correctAnswer?: string;
}

// Analytics Models
export interface FeedbackAnalytics {
  totalFeedbackCount: number;
  averageRating: number;
  fiveStarCount: number;
  fourStarCount: number;
  threeStarCount: number;
  twoStarCount: number;
  oneStarCount: number;
  feedbackByCategory: { [key: string]: number };
  feedbackByType: { [key: string]: number };
  monthlyTrends: MonthlyFeedback[];
  topRatedStudents: TopRatedEntity[];
  topRatedCompanies: TopRatedEntity[];
  topRatedCourses: TopRatedEntity[];
}

export interface MonthlyFeedback {
  month: string;
  count: number;
  averageRating: number;
}

export interface TopRatedEntity {
  id: number;
  name: string;
  averageRating: number;
  feedbackCount: number;
}

export interface AssessmentAnalytics {
  totalAttempts: number;
  averageScore: number;
  passCount: number;
  failCount: number;
  passRate: number;
  scoreDistribution: { [key: string]: number };
  questionAnalytics: QuestionAnalytics[];
}

export interface QuestionAnalytics {
  questionId: number;
  questionText: string;
  averageScore: number;
  correctAnswers: number;
  totalAttempts: number;
}

// Filter and Search Models
export interface FeedbackFilters {
  feedbackType?: string;
  category?: string;
  rating?: number;
  dateFrom?: Date;
  dateTo?: Date;
  entityId?: number;
  entityType?: string;
}

export interface AssessmentFilters {
  assessmentType?: string;
  targetAudience?: string;
  isActive?: boolean;
  createdByUserId?: number;
  dateFrom?: Date;
  dateTo?: Date;
}