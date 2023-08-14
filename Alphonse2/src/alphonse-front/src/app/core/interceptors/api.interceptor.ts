import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  private apiUrl: string = environment.apiUrl;

  constructor() {

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const apiRequest = request.clone({ url: `${this.apiUrl}${request.url}` });
    return next.handle(apiRequest);
  }
}
