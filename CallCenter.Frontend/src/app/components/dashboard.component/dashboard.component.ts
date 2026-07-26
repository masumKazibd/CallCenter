import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AgentService } from '../../services/agent.service'; 
import { CallService } from '../../services/call.service';
import { Agent } from '../../models/agent.model';
import { AuthService } from '../../services/auth.service';
import { SignalrService } from '../../services/signalr.service'; 
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';

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
  agents = signal<any[]>([]);
  allCalls = signal<any[]>([]);

  // Computed signal using the current agent ID to filter calls for that agent
  agentCalls = computed(() => {
    const currentId = this.authService.currentAgentId();
    if (!currentId) return []; 
    return this.allCalls().filter(c => c.agentId === currentId).sort((a, b) => b.id - a.id);
  });

  ngOnInit() {
    this.loadAgents();
    this.loadCalls();
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
    
    this.agentService.updateStatus(call.agentId, 'OnCall').subscribe({
      next: () => {
        console.log('✅ Agent status updated to OnCall');
        this.signalrService.incomingCall.set(null); // remove the popup after accepting
        this.loadCalls(); // refresh the call history to reflect the accepted call
      },
      error: (err) => console.error('❌ Failed to update agent status:', err)
    }); 
  }

  rejectCall() {
     const call = this.signalrService.incomingCall();
    if (!call) return;

    this.agentService.updateStatus(call.agentId, 'Available').subscribe({
      next: () => {
        console.log('✅ Agent status updated to Available');
        this.signalrService.incomingCall.set(null); // remove the popup after rejecting
        this.loadCalls(); // refresh the call history to reflect the rejected call
      },
      error: (err) => console.error('❌ Failed to update agent status:', err)
    });
  }
}