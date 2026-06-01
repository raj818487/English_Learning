import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ApiResponse } from '../models/common.model';
import { InterviewModel } from '../models/interview.model';

@Injectable({ providedIn: 'root' })
export class InterviewService {
	constructor(private readonly api: ApiService) {}

	list() {
		return this.api.get<ApiResponse<InterviewModel[]>>('/api/interview-questions');
	}


	create(payload: Partial<InterviewModel>) {
		return this.api.post<ApiResponse<InterviewModel>>('/api/interview-questions', payload);
	}

	update(id: string, payload: Partial<InterviewModel>) {
		return this.api.put<ApiResponse<InterviewModel>>(`/api/interview-questions/${id}`, payload);
	}

	delete(id: string) {
		return this.api.delete<ApiResponse<object>>(`/api/interview-questions/${id}`);
	}

}
