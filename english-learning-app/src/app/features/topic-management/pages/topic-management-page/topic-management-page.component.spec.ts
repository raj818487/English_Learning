import { render, screen } from '@testing-library/angular';
import { TopicManagementPageComponent } from './topic-management-page.component';

describe('TopicManagementPageComponent', () => {
  it('renders Topics title', async () => {
    await render(TopicManagementPageComponent);
    expect(screen.getByText('Topics')).toBeTruthy();
  });
});
