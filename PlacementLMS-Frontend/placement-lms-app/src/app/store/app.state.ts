import { AuthState } from './reducers/auth.reducer';

export interface AppState {
  auth: AuthState;
}

export interface LoadingState {
  isLoading: boolean;
  message?: string;
}

export interface ErrorState {
  hasError: boolean;
  errorMessage?: string;
}