import { Component, inject, OnInit } from '@angular/core';
import { AgentService } from '../../services/agent.service';

@Component({
  selector: 'app-dashboard.component',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit {
  private agentService = inject(AgentService);
  agents: any[] = [];

  ngOnInit() {
    this.fetchAgents();
  }

  fetchAgents() {
    this.agentService.getAgents().subscribe({
      next: (data: any) => {
        this.agents = data;
        console.log('Agents loaded successfully!', data);
      },
      error: (err) => {
        console.error('Failed to load agents:', err);
      }
    });
  }
}