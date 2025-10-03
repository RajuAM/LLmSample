import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FeedbackService } from '../../../../core/services/feedback.service';
import { CreateFeedbackDto, UpdateFeedbackDto, Feedback } from '../../../../core/models/feedback.model';

@Component({
  selector: 'app-feedback-form',
  templateUrl: './feedback-form.component.html',
  styleUrls: ['./feedback-form.component.css']
})
export class FeedbackFormComponent implements OnInit {
  feedbackForm!: FormGroup;
  isEditMode = false;
  feedbackId: number | null = null;
  loading = false;
  saving = false;
  error = '';

  feedbackTypes: string[] = [];
  categories: string[] = [];

  constructor(
    private fb: FormBuilder,
    private feedbackService: FeedbackService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.initializeForm();
  }

  ngOnInit(): void {
    this.feedbackTypes = this.feedbackService.getFeedbackTypes();
    this.categories = this.feedbackService.getFeedbackCategories();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.feedbackId = +id;
      this.loadFeedback(+id);
    }
  }

  initializeForm(): void {
    this.feedbackForm = this.fb.group({
      toUserId: ['', Validators.required],
      studentId: [''],
      companyId: [''],
      courseId: [''],
      jobApplicationId: [''],
      trainingSessionId: [''],
      feedbackType: ['', Validators.required],
      category: ['', Validators.required],
      rating: [5, [Validators.required, Validators.min(1), Validators.max(5)]],
      comments: ['', Validators.required],
      strengths: [''],
      areasForImprovement: [''],
      isAnonymous: [false]
    });
  }

  loadFeedback(id: number): void {
    this.loading = true;
    this.feedbackService.getFeedbackById(id).subscribe({
      next: (feedback) => {
        this.populateForm(feedback);
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load feedback';
        this.loading = false;
        console.error('Error loading feedback:', error);
      }
    });
  }

  populateForm(feedback: Feedback): void {
    this.feedbackForm.patchValue({
      toUserId: feedback.toUserId,
      studentId: feedback.studentId,
      companyId: feedback.companyId,
      courseId: feedback.courseId,
      jobApplicationId: feedback.jobApplicationId,
      trainingSessionId: feedback.trainingSessionId,
      feedbackType: feedback.feedbackType,
      category: feedback.category,
      rating: feedback.rating,
      comments: feedback.comments,
      strengths: feedback.strengths,
      areasForImprovement: feedback.areasForImprovement,
      isAnonymous: feedback.isAnonymous
    });
  }

  onSubmit(): void {
    if (this.feedbackForm.valid) {
      this.saving = true;
      this.error = '';

      if (this.isEditMode && this.feedbackId) {
        this.updateFeedback();
      } else {
        this.createFeedback();
      }
    } else {
      this.markFormGroupTouched();
    }
  }

  createFeedback(): void {
    const feedbackDto: CreateFeedbackDto = this.feedbackForm.value;

    this.feedbackService.createFeedback(feedbackDto).subscribe({
      next: () => {
        this.saving = false;
        this.router.navigate(['/feedback']);
      },
      error: (error) => {
        this.error = 'Failed to create feedback';
        this.saving = false;
        console.error('Error creating feedback:', error);
      }
    });
  }

  updateFeedback(): void {
    const feedbackDto: UpdateFeedbackDto = {
      category: this.feedbackForm.value.category,
      rating: this.feedbackForm.value.rating,
      comments: this.feedbackForm.value.comments,
      strengths: this.feedbackForm.value.strengths,
      areasForImprovement: this.feedbackForm.value.areasForImprovement,
      isPublished: true
    };

    this.feedbackService.updateFeedback(this.feedbackId!, feedbackDto).subscribe({
      next: () => {
        this.saving = false;
        this.router.navigate(['/feedback']);
      },
      error: (error) => {
        this.error = 'Failed to update feedback';
        this.saving = false;
        console.error('Error updating feedback:', error);
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/feedback']);
  }

  markFormGroupTouched(): void {
    Object.keys(this.feedbackForm.controls).forEach(key => {
      const control = this.feedbackForm.get(key);
      control?.markAsTouched();
    });
  }

  getRatingStars(rating: number): string {
    return '★'.repeat(rating) + '☆'.repeat(5 - rating);
  }

  setRating(rating: number): void {
    this.feedbackForm.patchValue({ rating });
  }
}