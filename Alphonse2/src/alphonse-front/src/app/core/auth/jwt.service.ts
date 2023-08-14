import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JwtService {
  private readonly constJwtTokenKeyName = 'jwtToken';

  constructor() { }

  getToken(): string | undefined {
    return window.localStorage[this.constJwtTokenKeyName];
  }

  saveToken(token: string): void {
    window.localStorage[this.constJwtTokenKeyName] = token;
  }

  destroyToken(): void {
    window.localStorage.removeItem(this.constJwtTokenKeyName);
  }
}
