import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { FeedbackListComponent } from './components/feedback-list/feedback-list.component';
import { FeedbackFormComponent } from './components/feedback-form/feedback-form.component';
import { FeedbackAnalyticsComponent } from './components/feedback-analytics/feedback-analytics.component';
import { AssessmentListComponent } from './components/assessment-list/assessment-list.component';
import { AssessmentFormComponent } from './components/assessment-form/assessment-form.component';
import { TakeAssessmentComponent } from './components/take-assessment/take-assessment.component';

const routes: Routes = [
  {
    path: '',
    component: FeedbackListComponent
  },
  {
    path: 'list',
    component: FeedbackListComponent
  },
  {
    path: 'create',
    component: FeedbackFormComponent
  },
  {
    path: 'edit/:id',
    component: FeedbackFormComponent
  },
  {
    path: 'analytics',
    component: FeedbackAnalyticsComponent
  },
  {
    path: 'assessments',
    component: AssessmentListComponent
  },
  {
    path: 'assessments/create',
    component: AssessmentFormComponent
  },
  {
    path: 'assessments/take/:id',
    component: TakeAssessmentComponent
  }
];

@NgModule({
  declarations: [
    FeedbackListComponent,
    FeedbackFormComponent,
    FeedbackAnalyticsComponent,
    AssessmentListComponent,
    AssessmentFormComponent,
    TakeAssessmentComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class FeedbackModule { }