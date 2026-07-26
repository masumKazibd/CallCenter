import { inject, Injectable, signal } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection !: signalR.HubConnection;
  private authService = inject(AuthService);

  incomingCall = signal<any | null>(null);
  
  public startConnection() {
    const backendUrl = `${environment.hubUrl}`;

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(backendUrl)
      .withAutomaticReconnect()
      .build();
 
    this.hubConnection.start().then(() => {
      console.log('✅ SignalR Connection Established Successfully!');
       
      this.addReceiveCallListener(); 
    })
    .catch(err => console.error('❌ Error while starting SignalR connection: ', err));
}

  private addReceiveCallListener() {
    this.hubConnection.on('ReceiveCall', (callData) => {
      const currentAgentId = this.authService.currentAgentId();
       
      if (currentAgentId && callData.agentId === currentAgentId) {
        this.incomingCall.set(callData);  // Update the incoming call signal with the new call data
      }
    });
  }
}