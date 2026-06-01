import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopicService } from '../../../../core/services/topic.service';

@Component({
  selector: 'app-topic-management-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './topic-management-page.component.html',
  styleUrls: ['./topic-management-page.component.scss']
})
export class TopicManagementPageComponent implements OnInit {
  topics: any[] = [];
  loading = true;

  constructor(private readonly topicService: TopicService) {}

  ngOnInit(): void {
    this.loadTopics();
  }

  private loadTopics() {
    this.loading = true;
    this.topicService.list().subscribe({
      next: res => {
        this.topics = (res as any).data || [];
        this.loading = false;
      },
      error: () => (this.loading = false)
    });
  }
}
