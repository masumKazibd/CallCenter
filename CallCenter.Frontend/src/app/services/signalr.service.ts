import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection: signalR.HubConnection | undefined;

  public startConnection() {
    const backendUrl = 'https://localhost:44301/callhub'; 

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(backendUrl)
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('✅ SignalR Connection Established Successfully!'))
      .catch(err => console.error('❌ Error while starting SignalR connection: ', err));
  }
}