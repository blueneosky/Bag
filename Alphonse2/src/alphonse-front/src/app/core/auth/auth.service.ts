import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, distinctUntilChanged, map, shareReplay, tap, throwError } from 'rxjs';
import { JwtService } from './jwt.service';
import { Router } from '@angular/router';
import { HttpClient } from "@angular/common/http";
import { User } from '@models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject
    .asObservable()
    .pipe(distinctUntilChanged());

  public get currentUser(): User | null { return this.currentUserSubject.value; }

  public isAuthenticated$ = this.currentUser$.pipe(
    map((user) => !!user),
  );
  public get isAuthenticated(): boolean { return !!this.currentUser; }

  constructor(
    private readonly http: HttpClient,
    private readonly jwtService: JwtService,
    private readonly router: Router
  ) { }

  login(credentials: { userName: string, userPass: string }): Observable<User> {
    return this.http
      .post('/security/login', credentials, { responseType: 'text' as const })
      .pipe(
        map((token) => ({ token: token, userName: credentials.userName })),
        tap((user) => this.setAuth(user))
      );
  }

  getCurrentUser(): Observable<User> {
    const token = this.jwtService.getToken();
    if (!token) return throwError(() => new Error('token missing'));

    return this.http
      .get('/security/userName', { responseType: 'text' as const })
      .pipe(
        map((userName) => ({ userName: userName, token: token })),
        tap({
          next: (user) => this.setAuth(user),
          error: () => this.purgeAuth(),
        }),
        shareReplay(1),
      );
  }

  logout(): void {
    this.purgeAuth();
    void this.router.navigate(['/']);
  }

  private setAuth(user: User): void {
    this.jwtService.saveToken(user.token);
    if (this.currentUserSubject.value != user)
      this.currentUserSubject.next(user);
  }

  purgeAuth(): void {
    console.log("Auth purged");

    this.jwtService.destroyToken();
    this.currentUserSubject.next(null);
  }
}
