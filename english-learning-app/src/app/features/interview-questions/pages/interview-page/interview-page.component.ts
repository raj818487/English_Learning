import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InterviewService } from '../../../../core/services/interview.service';
import { AuthService } from '../../../../core/services/auth.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-interview-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './interview-page.component.html',
  styleUrls: ['./interview-page.component.scss']
})
export class InterviewPageComponent implements OnInit {
  interviews: any[] = [];
  loading = true;
  isAdmin = false;

  form = this.fb.group({
    title: [''],
    text: [''],
    suggestedAnswer: ['']
  });

  constructor(private readonly interviewService: InterviewService, private readonly auth: AuthService, private readonly fb: FormBuilder) {}

  ngOnInit(): void {
    this.load();
    this.isAdmin = !!this.auth.getToken();
  }

  private load() {
    this.loading = true;
    this.interviewService.list().subscribe({
      next: res => {
        this.interviews = (res as any).data || [];
        this.loading = false;
      },
      error: () => (this.loading = false)
    });
  }

  create() {
    const payload = this.form.value;
    this.interviewService.create(payload).subscribe({ next: () => { this.form.reset(); this.load(); } });
  }

  remove(id: string) {
    if (!confirm('Delete this question?')) return;
    this.interviewService.delete(id).subscribe({ next: () => this.load() });
  }

  edit(id: string) {
    // For brevity, prompt for a new title; a full edit modal is recommended
    const title = prompt('New title');
    if (title === null) return;
    this.interviewService.update(id, { title }).subscribe({ next: () => this.load() });
  }
}
