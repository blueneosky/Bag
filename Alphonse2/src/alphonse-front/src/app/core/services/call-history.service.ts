import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CallHistory } from '@models/call-history.model';
import { PagedResult } from '@models/paged-result.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CallHistoryService {

  constructor(
    private readonly http: HttpClient
  ) { }

  get(pageIndex: number, pageSize: number): Observable<PagedResult<CallHistory>> {
    let params = new HttpParams()
      .set('pageIndex', pageIndex)
      .set('pageSize', pageSize);
    
    return this.http.get<PagedResult<CallHistory>>(
      '/CallHistory', 
      { params: params }
    );
  }
  
}
