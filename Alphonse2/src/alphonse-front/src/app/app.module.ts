import { APP_INITIALIZER, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { EMPTY } from 'rxjs';
import { registerLocaleData } from '@angular/common';

import { AuthService } from '@auth/auth.service';
import { JwtService } from '@auth/jwt.service';
import { environment } from '@environments/environment';
import { FeaturesModule } from '@features/features.module';
import { ApiInterceptor } from '@interceptors/api.interceptor';
import { ErrorInterceptor } from '@interceptors/error.interceptor';
import { TokenInterceptor } from '@interceptors/token.interceptor';
import { SharedModule } from '@shared/shared.module';

import localeFr from '@angular/common/locales/fr';
registerLocaleData(localeFr);

export function initAuth(jwtService: JwtService, authService: AuthService) {
  return () => (jwtService.getToken() ? authService.getCurrentUser() : EMPTY);
}

export function disableConsoleOnProduction() {
  return () => {
    if (environment.production) {
      console.warn(`🚨 Console output is disabled on production!`);
      console.log = function (): void { };
      console.debug = function (): void { };
      console.warn = function (): void { };
      console.info = function (): void { };
    }
  }
}


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    FeaturesModule,
  ],
  exports: [
    SharedModule,
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'fr' },
    { provide: APP_INITIALIZER, useFactory: disableConsoleOnProduction, multi: true, },
    {
      provide: APP_INITIALIZER,
      useFactory: initAuth,
      deps: [JwtService, AuthService],
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
