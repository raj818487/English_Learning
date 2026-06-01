import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-auth-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.scss']
})
export class AuthPageComponent {
  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  loading = false;
  errorMessage = '';

  constructor(private readonly fb: FormBuilder, private readonly auth: AuthService) {}

  submit() {
    if (this.form.invalid) return;
    this.loading = true;
    this.errorMessage = '';
    const { email, password } = this.form.value as { email: string; password: string };
    this.auth.login(email, password).subscribe({
      next: () => {
        this.loading = false;
        // TODO: navigate to dashboard after successful login
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = err?.message || 'Login failed';
      }
    });
  }
}
