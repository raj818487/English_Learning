import { render } from '@testing-library/angular';
import { DashboardPageComponent } from './dashboard-page.component';

describe('DashboardPageComponent', () => {
  it('renders title', async () => {
    const { getByText } = await render(DashboardPageComponent);
    expect(getByText('Welcome to English Learning')).toBeTruthy();
  });
});
