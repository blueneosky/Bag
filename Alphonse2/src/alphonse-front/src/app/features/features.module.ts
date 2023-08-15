import { NgModule } from '@angular/core';
import { DatePipe } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';

import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { PhoneBookComponent } from './phone-book/phone-book.component';

@NgModule({
  declarations: [
    HomeComponent,
    LoginComponent,
    PhoneBookComponent,
  ],
  imports: [
    SharedModule,
    MatTableModule,
    MatPaginatorModule,
    MatCardModule,
    MatProgressBarModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
  
    DatePipe,
  ]
})
export class FeaturesModule { }
