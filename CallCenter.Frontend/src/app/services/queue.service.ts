import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Service } from '@angular/core';
import { Observable } from 'rxjs'; 
import { Queue } from '../models/queue.model'; 

@Injectable(
    {
        providedIn: 'root'
    }
)
export class QueueService {
    private http = inject(HttpClient);
  
    private baseUrl = 'https://localhost:44301/api/Queues'; 
    getQueues(): Observable<Queue[]> {
        return this.http.get<Queue[]>(this.baseUrl);   
    }
}
