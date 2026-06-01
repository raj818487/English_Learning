import { render, screen } from '@testing-library/angular';
import { AuthPageComponent } from './auth-page.component';

describe('AuthPageComponent', () => {
  it('shows sign in title', async () => {
    await render(AuthPageComponent);
    expect(screen.getByText('Sign in')).toBeTruthy();
  });
});
