import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { JobOpportunitiesComponent } from './components/job-opportunities/job-opportunities.component';

const routes: Routes = [
  {
    path: '',
    component: JobOpportunitiesComponent
  },
  {
    path: 'jobs',
    component: JobOpportunitiesComponent
  }
];

@NgModule({
  declarations: [
    JobOpportunitiesComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class PlacementModule { }