import { render, screen } from '@testing-library/angular';
import { InterviewPageComponent } from './interview-page.component';

describe('InterviewPageComponent', () => {
  it('renders Interview Questions title', async () => {
    await render(InterviewPageComponent);
    expect(screen.getByText('Interview Questions')).toBeTruthy();
  });
});
