import { MediaMatcher } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLinkActive } from '@angular/router';
import { AuthService } from '@auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'alphonse-front';
  mobileQuery: MediaQueryList;

  public get isAuthenticated$() { return this.authService.isAuthenticated$; }

  navItems: { label: string, routerLink: string, matIcon: string }[] = [
    { label: 'Home', routerLink: '/home', matIcon: 'home' },
    { label: 'Phone book', routerLink: '/phoneBook', matIcon: 'import_contacts' },
  ]

  constructor(
    private readonly authService: AuthService,
    private readonly router: Router,
    mediaMatcher: MediaMatcher
  ) {
    this.mobileQuery = mediaMatcher.matchMedia('(max-width: 600px)');
  }

  ngOnInit(): void {
  }

  logoutClick() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
