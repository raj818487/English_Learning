import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
	constructor(private readonly api: ApiService) {}

	login(email: string, password: string) {
		return this.api.post<{ token: string; role?: string }>('/api/auth/login', { email, password }).pipe(
			tap(res => {
				if ((res as any)?.token) {
					localStorage.setItem('auth.token', (res as any).token);
					if ((res as any).role) localStorage.setItem('auth.role', (res as any).role);
				}
			})
		);
	}

	logout() {
		localStorage.removeItem('auth.token');
		localStorage.removeItem('auth.role');
	}

	getToken() {
		return localStorage.getItem('auth.token');
	}

	getRole() {
		return localStorage.getItem('auth.role');
	}
}
