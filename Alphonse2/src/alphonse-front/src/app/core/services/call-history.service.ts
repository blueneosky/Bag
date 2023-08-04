import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtService } from './jwt.service';

@Injectable({
  providedIn: 'root'
})
export class CallHistoryService {

  constructor(
    private readonly http: HttpClient,
    private readonly jwtService: JwtService
  ) { }

  
}
