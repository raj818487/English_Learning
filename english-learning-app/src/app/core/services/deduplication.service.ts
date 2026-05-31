import { Injectable } from '@angular/core';
import { ContentModel } from '../models/content.model';

@Injectable({ providedIn: 'root' })
export class DeduplicationService {
  isDuplicate(candidate: ContentModel, existing: ContentModel[]): boolean {
    const normalized = `${candidate.title}|${candidate.text}`.toLowerCase().trim();
    return existing.some(item => `${item.title}|${item.text}`.toLowerCase().trim() === normalized);
  }
}
