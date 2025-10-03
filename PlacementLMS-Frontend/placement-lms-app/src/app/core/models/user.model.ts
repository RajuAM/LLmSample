export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: Date;
  address: string;
  userType: string;
  isActive: boolean;
  createdAt: Date;
  updatedAt: Date;
  refreshToken?: string;
  refreshTokenExpiryTime?: Date;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber: string;
  dateOfBirth: Date;
  address: string;
  userType: string;
}

export interface AuthResponse {
  token: string;
  user: User;
  expiresIn: number;
}