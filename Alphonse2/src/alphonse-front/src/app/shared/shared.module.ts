import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './components/footer/footer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';


@NgModule({
  imports: [
    CommonModule, FormsModule, ReactiveFormsModule,
    MatButtonModule, MatIconModule,
  ],
  declarations: [
    FooterComponent,
  ],
  exports: [
    FooterComponent,

    // commoners
    CommonModule, FormsModule, ReactiveFormsModule,
    MatButtonModule, MatIconModule,
  ]
})
export class SharedModule { }
