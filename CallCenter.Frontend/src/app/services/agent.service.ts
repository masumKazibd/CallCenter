import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AgentService {
  private http = inject(HttpClient);
  
  
  private apiUrl = 'https://localhost:44301/api/Agents'; 

  getAgents() {
    return this.http.get(this.apiUrl);
  }

  createAgent(agentData: any) {
    return this.http.post(this.apiUrl, agentData);
  } 
    updateStatus(id: number, status: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/status`, { status });
    }

    deleteAgent(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}