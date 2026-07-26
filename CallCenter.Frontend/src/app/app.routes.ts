import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard.component/dashboard.component';
import { AgentComponent } from './components/agent.component/agent.component';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'agent', component: AgentComponent },
];