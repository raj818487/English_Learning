import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopicService } from '../../../../core/services/topic.service';

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {
  topicsCount = 0;
  loading = true;

  constructor(private readonly topicService: TopicService) {}

  ngOnInit(): void {
    this.loadCounts();
  }

  private loadCounts() {
    this.loading = true;
    this.topicService.list().subscribe({
      next: res => {
        this.topicsCount = Array.isArray((res as any).data) ? (res as any).data.length : 0;
        this.loading = false;
      },
      error: () => (this.loading = false)
    });
  }
}
