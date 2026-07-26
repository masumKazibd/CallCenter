import { Agent } from './agent.model';
import { Queue } from './queue.model';  
import { CallEvent } from './call-event.model';
import { CallDirection, CallStatus } from './enums.model';

export interface Call {
  id: number; 
  direction: CallDirection;  
  fromNumber: string;  
  toNumber: string;  
  status: CallStatus;  
  
  agentId?: number | null;  
  agent?: Agent;  
  queueId?: number | null; 
  queue?: Queue; 
  
  startedAt: string; 
  answeredAt?: string | null; 
  endedAt?: string | null; 
  durationSeconds: number; 
  
  recordingUrl?: string | null; 
  crmCustomerId?: string | null; 
  
  events?: CallEvent[]; 
}