import { Agent } from './agent.model';

export interface Queue {
  id: number; 
  name: string;  
  description: string; 
  agents?: Agent[];  
  createdAt: string; 
}