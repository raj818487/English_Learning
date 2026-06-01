import { Routes } from '@angular/router';

export const routes: Routes = [
	{
		path: '',
		loadComponent: () => import('./features/dashboard/pages/dashboard-page/dashboard-page.component').then(m => m.DashboardPageComponent)
	}
	,{
		path: 'auth',
		loadComponent: () => import('./features/auth/pages/auth-page/auth-page.component').then(m => m.AuthPageComponent)
	}
	,{
		path: 'topics',
		loadComponent: () => import('./features/topic-management/pages/topic-management-page/topic-management-page.component').then(m => m.TopicManagementPageComponent)
	}
	,{
		path: 'interviews',
		loadComponent: () => import('./features/interview-questions/pages/interview-page/interview-page.component').then(m => m.InterviewPageComponent)
	}
	,{
		path: 'content',
		loadComponent: () => import('./features/content-management/pages/content-management-page/content-management-page.component').then(m => m.ContentManagementPageComponent)
	}
];
