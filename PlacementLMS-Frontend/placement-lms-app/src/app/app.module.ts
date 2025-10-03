import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

// Store and effects
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

// Components
import { AppComponent } from './app.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { CalendarComponent } from './shared/components/calendar/calendar.component';

// Auth Components
import { LoginComponent } from './modules/auth/login/login.component';

// Student Components
import { StudentDashboardComponent } from './modules/student/dashboard/dashboard.component';

// Module imports
import { CourseModule } from './modules/course/course.module';
import { PlacementModule } from './modules/placement/placement.module';

// Define routes inline for now
const routes: Routes = [
  { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
  {
    path: 'auth',
    children: [
      { path: 'login', component: LoginComponent }
    ]
  },
  {
    path: 'student',
    children: [
      { path: 'dashboard', component: StudentDashboardComponent }
    ]
  },
  {
    path: 'courses',
    loadChildren: () => import('./modules/course/course.module').then(m => m.CourseModule)
  },
  {
    path: 'placement',
    loadChildren: () => import('./modules/placement/placement.module').then(m => m.PlacementModule)
  },
  { path: '**', redirectTo: '/auth/login' }
];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    CalendarComponent,
    LoginComponent,
    StudentDashboardComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: false
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
