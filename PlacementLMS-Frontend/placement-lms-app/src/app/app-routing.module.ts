import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Auth Guards (to be created)
import { AuthGuard } from './core/guards/auth.guard';
import { RoleGuard } from './core/guards/role.guard';

// Components
import { LoginComponent } from './modules/auth/login/login.component';
import { StudentDashboardComponent } from './modules/student/dashboard/dashboard.component';

const routes: Routes = [
  // Default redirect
  { path: '', redirectTo: '/auth/login', pathMatch: 'full' },

  // Authentication routes
  {
    path: 'auth',
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: LoginComponent }, // Using same component for demo
      { path: 'forgot-password', component: LoginComponent } // Using same component for demo
    ]
  },

  // Student routes
  {
    path: 'student',
    canActivate: [AuthGuard],
    data: { roles: ['Student'] },
    children: [
      { path: 'dashboard', component: StudentDashboardComponent },
      { path: 'profile', component: StudentDashboardComponent }, // Placeholder
      { path: 'courses', component: StudentDashboardComponent }, // Placeholder
      { path: 'assignments', component: StudentDashboardComponent }, // Placeholder
      { path: 'tests', component: StudentDashboardComponent }, // Placeholder
      { path: 'resume', component: StudentDashboardComponent }, // Placeholder
      { path: 'jobs', component: StudentDashboardComponent } // Placeholder
    ]
  },

  // Admin routes
  {
    path: 'admin',
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Admin'] },
    loadChildren: () => import('./modules/admin/admin.module').then(m => m.AdminModule)
  },

  // Institution routes
  {
    path: 'institution',
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Institution'] },
    children: [
      { path: 'dashboard', component: StudentDashboardComponent }, // Placeholder
      { path: 'courses', component: StudentDashboardComponent }, // Placeholder
      { path: 'students', component: StudentDashboardComponent }, // Placeholder
      { path: 'assignments', component: StudentDashboardComponent }, // Placeholder
      { path: 'reports', component: StudentDashboardComponent } // Placeholder
    ]
  },

  // Industry routes
  {
    path: 'industry',
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Industry'] },
    children: [
      { path: 'dashboard', component: StudentDashboardComponent }, // Placeholder
      { path: 'jobs', component: StudentDashboardComponent }, // Placeholder
      { path: 'applications', component: StudentDashboardComponent }, // Placeholder
      { path: 'interviews', component: StudentDashboardComponent }, // Placeholder
      { path: 'analytics', component: StudentDashboardComponent } // Placeholder
    ]
  },

  // Feedback routes (available to all authenticated users)
  {
    path: 'feedback',
    canActivate: [AuthGuard],
    loadChildren: () => import('./modules/feedback/feedback.module').then(m => m.FeedbackModule)
  },

  // Wildcard route
  { path: '**', redirectTo: '/auth/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
