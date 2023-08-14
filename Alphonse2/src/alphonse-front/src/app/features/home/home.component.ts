import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { AuthService } from '@auth/auth.service';
import { CallHistory } from '@models/call-history.model';
import { PagedResult } from '@models/paged-result.model';
import { CallHistoryService } from '@services/call-history.service';
import { catchError, filter, map, of, tap } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public readonly displayedColumns: string[] = ['phoneNumber', 'timestamp', 'event'];
  public currentHistoryPage: PagedResult<CallHistory> | undefined;
  public nbTotalResults: number = 0;
  public pageSize: number = 10;
  public pageIndex: number = 0;
  public results: CallHistory[] = [];

  constructor(
    private callHistoryService: CallHistoryService,
    private authService: AuthService
  ) {
  }

  ngOnInit(): void {
    if (!this.currentHistoryPage)
      this.updateData(0, this.pageSize);
  }

  handlePageEvent(event: PageEvent) {
    this.updateData(event.pageIndex, event.pageSize);
  }

  updateData(pageIndex: number, pageSize: number) {
    this.callHistoryService.get(pageIndex, pageSize)
      .pipe(catchError(err => {
        console.log('Call history error:', err);
        return of(undefined);
      }))
      .subscribe(pr => {
        this.currentHistoryPage = pr;
        this.nbTotalResults = this.currentHistoryPage?.nbTotalResults ?? 0;
        this.pageSize = this.currentHistoryPage?.pageSize ?? 0;
        this.pageIndex = this.currentHistoryPage?.pageIndex ?? 0;
        this.results = this.currentHistoryPage?.results ?? [];
      })
  }

}
