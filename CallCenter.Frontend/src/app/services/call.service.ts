import { Injectable, inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class CallService {
    private baseUrl = `${environment.apiUrl}/calls`;
    private http = inject(HttpClient);

    getCalls(): Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl);
    }

    updateCallStatus(id: number, status: string): Observable<any> {
        return this.http.put(`${this.baseUrl}/${id}/status`, { status });
    }
}
