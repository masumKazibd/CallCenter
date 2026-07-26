import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { Agent } from '../../models/agent.model';
import { AgentService } from '../../services/agent.service';
import { FormsModule } from '@angular/forms';
import { QueueService } from '../../services/queue.service';

@Component({
  selector: 'app-agent.component',
  imports: [
    FormsModule
  ],
  templateUrl: './agent.component.html',
  styleUrl: './agent.component.css',
})
export class AgentComponent implements OnInit {
  private agentService = inject(AgentService);
  private queueService = inject(QueueService); 
  private cdr = inject(ChangeDetectorRef);

  queues: any[] = [];
  agents: Agent[] = []; 

  statuses = ['Offline', 'Available', 'OnCall', 'Wrapup', 'NotReady'];
  newAgent = { fullName: '', email: '', extension: '', status: 'Offline', queueId: null as number | null };

  ngOnInit() {
    this.fetchAgents();
    this.fetchQueues();
  }
  fetchQueues() {
    this.queueService.getQueues().subscribe({
      next: (data) => this.queues = data,
      error: (err) => console.error('Failed to load queues:', err)
    });
  }
  fetchAgents() {
    this.agentService.getAgents().subscribe({
      next: (data) => {
        this.agents = data;
        this.cdr.detectChanges();  
      },
      error: (err) => console.error('Failed to load agents:', err)
    });
  }
  createAgent() {
    this.agentService.createAgent(this.newAgent).subscribe({
      next: () => {
        this.newAgent = { fullName: '', email: '', extension: '', status: 'Offline', queueId: null };
        this.fetchAgents();
      },
      error: (err) => console.error('Failed to create agent:', err)
    });
  }

  changeStatus(agent: Agent, status: string) {
    this.agentService.updateStatus(agent.id, status).subscribe({
      next: () => agent.status = status,
      error: (err) => console.error('Failed to update status:', err)
    });
  }

  deleteAgent(id: number) {
    this.agentService.deleteAgent(id).subscribe({
      next: () => this.fetchAgents(),
      error: (err) => console.error('Failed to delete agent:', err)
    });
  }

  statusClass(status: string): string {
    switch (status) {
      case 'Available': return 'bg-success';
      case 'OnCall':    return 'bg-danger';
      case 'Wrapup':    return 'bg-warning text-dark';
      case 'NotReady':  return 'bg-secondary';
      default:          return 'bg-info text-dark'; // Offline
    }
  }
}