import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ContentModel } from '../models/content.model';
import { ApiResponse } from '../models/common.model';

@Injectable({ providedIn: 'root' })
export class ContentService {
  constructor(private readonly api: ApiService) {}

  search(query: string) {
    return this.api.get<ApiResponse<ContentModel[]>>(`/api/content/search?query=${encodeURIComponent(query)}`);
  }
}
