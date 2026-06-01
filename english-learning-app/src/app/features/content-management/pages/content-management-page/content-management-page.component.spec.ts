import { render, screen } from '@testing-library/angular';
import { ContentManagementPageComponent } from './content-management-page.component';

describe('ContentManagementPageComponent', () => {
  it('renders Content Management title', async () => {
    await render(ContentManagementPageComponent);
    expect(screen.getByText('Content Management')).toBeTruthy();
  });
});
