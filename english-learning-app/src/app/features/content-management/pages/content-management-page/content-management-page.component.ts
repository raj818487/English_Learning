import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentService } from '../../../../core/services/content.service';

@Component({
  selector: 'app-content-management-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './content-management-page.component.html',
  styleUrls: ['./content-management-page.component.scss']
})
export class ContentManagementPageComponent implements OnInit {
  contents: any[] = [];
  loading = true;

  constructor(private readonly contentService: ContentService) {}

  ngOnInit(): void {
    this.load('');
  }

  load(query: string) {
    this.loading = true;
    this.contentService.search(query).subscribe({
      next: res => {
        this.contents = (res as any).data || [];
        this.loading = false;
      },
      error: () => (this.loading = false)
    });
  }
}
