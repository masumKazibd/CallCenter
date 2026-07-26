import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Service } from '@angular/core';
import { Observable } from 'rxjs'; 
import { Queue } from '../models/queue.model'; 
import { environment } from '../../environments/environment';

@Injectable(
    {
        providedIn: 'root'
    }
)
export class QueueService {
    private http = inject(HttpClient);
 
    private baseUrl = `${environment.apiUrl}/Queues`;
    getQueues(): Observable<Queue[]> {
        return this.http.get<Queue[]>(this.baseUrl);   
    }
}
