import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AgentService } from '../../services/agent.service'; 
import { Agent } from '../../models/agent.model';

@Component({
  selector: 'app-dashboard',
  imports: [FormsModule],   // 👈 needed for [(ngModel)]
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit {
  private agentService = inject(AgentService);

  agents: Agent[] = []; 
  statuses = ['Offline', 'Available', 'OnCall', 'Wrapup', 'NotReady'];
  newAgent = { fullName: '', email: '', extension: '', queueId: null as number | null };

  ngOnInit() {
    this.fetchAgents();
  }

  fetchAgents() {
    this.agentService.getAgents().subscribe({
      next: (data) => this.agents = data,
      error: (err) => console.error('Failed to load agents:', err)
    });
  }

  createAgent() {
    this.agentService.createAgent(this.newAgent).subscribe({
      next: () => {
        this.newAgent = { fullName: '', email: '', extension: '', queueId: null };
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