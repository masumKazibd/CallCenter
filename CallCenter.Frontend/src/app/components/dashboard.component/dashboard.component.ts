import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AgentService } from '../../services/agent.service'; 
import { CallService } from '../../services/call.service';
import { Agent } from '../../models/agent.model';
import { AuthService } from '../../services/auth.service';
import { SignalrService } from '../../services/signalr.service'; 
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-dashboard',
  imports: [
    CommonModule, 
    FormsModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit {
  public authService = inject(AuthService);
  public signalrService = inject(SignalrService);
  private agentService = inject(AgentService); 
  private callService = inject(CallService);
  private customerService = inject(CustomerService);
  
  agents = signal<any[]>([]);
  allCalls = signal<any[]>([]);
  activeCall = signal<any | null>(null);  // for tracking the call time or call timer 
  callDuration = signal<number>(0);
  timerInterval: any;


  //for customer search & display
  searchQuery = signal<string>('');
  customerInfo = signal<any | null>(null);
  isSearching = signal<boolean>(false);
  searchError = signal<string>('');



  // Computed signal using the current agent ID to filter calls for that agent
  agentCalls = computed(() => {
    const currentId = this.authService.currentAgentId();
    if (!currentId) return []; 
    return this.allCalls().filter(c => c.agentId === currentId).sort((a, b) => b.id - a.id);
  });

  ngOnInit() {
    this.loadAgents();
    this.loadCalls();
    this.signalrService.startConnection();
  }

  loadAgents() { 
    this.agentService.getAgents().subscribe({
      next: (res) => this.agents.set(res),
      error: (err) => console.error('❌ Failed to load agents', err)
    }); 
  }

  loadCalls() {
    this.callService.getCalls().subscribe({
      next: (res) => this.allCalls.set(res),
      error: (err) => console.error('❌ Failed to load calls', err)
    }) 
  }

  onAgentChange(event: any) {
    const agentId = Number(event.target.value);
    if (agentId) {
      this.authService.setAgent(agentId);
      this.loadCalls(); // if agent changes, refresh the call history for that agent
    } else {
      this.authService.setAgent(null as any);
    }
  }

  acceptCall() {
    const call = this.signalrService.incomingCall();
    if (!call) return;
     
    const callId = call.id || call.Id;
    const agentId = call.agentId || call.AgentId;

    this.agentService.updateStatus(agentId, 'OnCall').subscribe({
      next: () => { 
        this.callService.updateCallStatus(callId, 'InProgress').subscribe({
          next: () => {
            console.log('✅ Call in progress');
            this.activeCall.set(call);
            this.signalrService.incomingCall.set(null);
            this.startTimer();
            this.loadCalls();
          },
          error: (err) => console.error('❌ Failed to update call status to InProgress:', err)
        });
      },
      error: (err) => console.error('❌ Failed to update agent status:', err)
    }); 
  }

  rejectCall() {
    const call = this.signalrService.incomingCall();
    if (!call) return;

    const callId = call.id || call.Id;
    const agentId = call.agentId || call.AgentId;

    this.callService.updateCallStatus(callId, 'Rejected').subscribe({
      next: () => { 
        this.agentService.updateStatus(agentId, 'Available').subscribe({
          next: () => {
            console.log('✅ Call Rejected & Agent is Available');
            this.signalrService.incomingCall.set(null);
            this.loadCalls(); 
          },
          error: (err) => console.error('❌ Failed to update agent status:', err)
        });
      }, 
      error: (err) => console.error('❌ Failed to update call status to Rejected:', err)
    });
  }

  endCall() {
    const call = this.activeCall();
    if (!call) return;

    const callId = call.id || call.Id;
    const agentId = call.agentId || call.AgentId;

    this.stopTimer();
 
    this.callService.updateCallStatus(callId, 'Completed').subscribe({
      next: () => { 
        this.agentService.updateStatus(agentId, 'Available').subscribe({
          next: () => {
            console.log('✅ Call Ended & Agent is Available');
            this.activeCall.set(null); 
            this.loadCalls(); 
          },
          error: (err) => console.error('❌ Failed to update agent status:', err)
        });
      },
      error: (err) => console.error('❌ Failed to update call status to Completed:', err)
    });
  }

  // --- Timer Helper Methods ---
  startTimer() {
    this.callDuration.set(0);
    this.timerInterval = setInterval(() => { 
      this.callDuration.update(val => val + 1);
    }, 1000);
  }

  stopTimer() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
  }

  get formattedCallTime(): string {
    const totalSeconds = this.callDuration();
    const minutes = Math.floor(totalSeconds / 60).toString().padStart(2, '0');
    const seconds = (totalSeconds % 60).toString().padStart(2, '0');
    return `${minutes}:${seconds}`;
  }

  searchCustomer() {
    const query = this.searchQuery().trim();
    if (!query) return;

    this.isSearching.set(true);
    this.searchError.set('');
    this.customerInfo.set(null);
 
    this.customerService.searchCustomer(query).subscribe({
      next: (data) => {
        if (data) {
          this.customerInfo.set(data);
        } else { 
          this.searchError.set('No customer found with this name or phone number.');
        }
        this.isSearching.set(false);
      },
      error: () => {
        this.searchError.set('An error occurred while searching.');
        this.isSearching.set(false);
      }
    });
  }
}