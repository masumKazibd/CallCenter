import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard.component/dashboard.component';
import { AgentComponent } from './components/agent.component/agent.component';
import { CallSimulatorComponent } from './components/call-simulator.component/call-simulator.component';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'agent', component: AgentComponent },
  { path: 'call-simulator', component: CallSimulatorComponent }
];