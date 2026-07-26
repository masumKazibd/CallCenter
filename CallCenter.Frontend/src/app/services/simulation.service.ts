import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SimulationService {
  private http = inject(HttpClient);

  private baseUrl = `${environment.apiUrl}/simulation`; 

  generateCall(callData: { customerPhoneNumber: string; queueId: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}/generate`, callData);
  }
}