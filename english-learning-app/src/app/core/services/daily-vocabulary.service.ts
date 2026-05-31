import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ApiResponse } from '../models/common.model';
import { DailyVocabularyWordModel } from '../models/daily-vocabulary.model';

@Injectable({ providedIn: 'root' })
export class DailyVocabularyService {
  constructor(private readonly api: ApiService) {}

  generate(date: string, count = 50) {
    return this.api.post<ApiResponse<DailyVocabularyWordModel[]>>(
      `/api/vocabulary/daily/generate?count=${count}&date=${encodeURIComponent(date)}`,
      {}
    );
  }

  getByDate(date: string) {
    return this.api.get<ApiResponse<DailyVocabularyWordModel[]>>(`/api/vocabulary/daily?date=${encodeURIComponent(date)}`);
  }
}
