import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Agent } from '../models/agent.model';

@Injectable({
  providedIn: 'root'
})
export class AgentService {
  private http = inject(HttpClient);
  
  
  private baseUrl = 'https://localhost:44301/api/Agents'; 

  getAgents(): Observable<Agent[]> {
    return this.http.get<Agent[]>(this.baseUrl);   
  }

  createAgent(agent: Partial<Agent>): Observable<Agent> {
    return this.http.post<Agent>(this.baseUrl, agent);
  }

  updateStatus(id: number, status: string): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}/status`, { status });
  }

  deleteAgent(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}