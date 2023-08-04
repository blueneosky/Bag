import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, distinctUntilChanged, map, shareReplay, tap } from 'rxjs';
import { UserModel } from '../models/user.model';
import { JwtService } from './jwt.service';
import { Router } from '@angular/router';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  private currentUserSubject = new BehaviorSubject<UserModel | null>(null);
  public currentUser = this.currentUserSubject
    .asObservable()
    .pipe(distinctUntilChanged());

  public isAuthenticated = this.currentUser.pipe(map((user) => !!user));

  constructor(
    private readonly http: HttpClient,
    private readonly jwtService: JwtService,
    private readonly router: Router
  ) { }

  login(credentials: { userName: string, userPass: string }): Observable<UserModel> {
    return this.http
      .post<string>('/security/login', credentials)
      .pipe(
        map((token) => ({ token: token, userName: credentials.userName })),
        tap((user) => this.setAuth(user))
      );
  }

  getCurrentUser(): Observable<UserModel> {
    return this.http
      .get<string>('/security/userName')
      .pipe(
        map((userName) => ({ userName: userName, token: this.jwtService.getToken() })),
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

  private setAuth(user: UserModel): void {
    console.log(`Auth set to ${user.token}`);
    
    this.jwtService.saveToken(user.token);
    this.currentUserSubject.next(user);
  }

  purgeAuth(): void {
    console.log("Auth purged");

    this.jwtService.destroyToken();
    this.currentUserSubject.next(null);
  }
}
