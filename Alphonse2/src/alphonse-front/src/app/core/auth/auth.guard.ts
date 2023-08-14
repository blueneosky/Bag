import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

export const authGuard = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isAuthenticated)
    return true;

  // Redirect to login page
  router.navigate(['/login'], { queryParams: { returnUrl: router.url } });
  return false;
}