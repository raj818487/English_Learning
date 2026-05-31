import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DailyVocabularyService } from './core/services/daily-vocabulary.service';
import { DailyVocabularyWordModel } from './core/models/daily-vocabulary.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'english-learning-app';
  selectedDate = this.getTodayDate();
  dailyWords: DailyVocabularyWordModel[] = [];
  isLoading = false;
  errorMessage = '';

  constructor(private readonly dailyVocabularyService: DailyVocabularyService) {}

  generateWords() {
    this.errorMessage = '';
    this.isLoading = true;
    this.dailyVocabularyService.generate(this.selectedDate).subscribe({
      next: (response) => {
        this.dailyWords = response.data;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Failed to generate daily words.';
        this.isLoading = false;
      }
    });
  }

  loadWordsByDate() {
    this.errorMessage = '';
    this.isLoading = true;
    this.dailyVocabularyService.getByDate(this.selectedDate).subscribe({
      next: (response) => {
        this.dailyWords = response.data;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Failed to load daily words.';
        this.isLoading = false;
      }
    });
  }

  private getTodayDate(): string {
    return new Date().toISOString().split('T')[0];
  }
}
