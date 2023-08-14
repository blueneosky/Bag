import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtService } from '@auth/jwt.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(
    private readonly jwtService: JwtService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.jwtService.getToken();
    if (!token)
      return next.handle(request);
    
    const tokenizedRequest = request.clone({
      setHeaders: {
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
      },
    });
    return next.handle(tokenizedRequest);
  }
}
