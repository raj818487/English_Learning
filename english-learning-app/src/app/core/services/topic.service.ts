import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { TopicModel } from '../models/topic.model';
import { ApiResponse } from '../models/common.model';

@Injectable({ providedIn: 'root' })
export class TopicService {
  constructor(private readonly api: ApiService) {}

  list() {
    return this.api.get<ApiResponse<TopicModel[]>>('/api/topics');
  }
}
