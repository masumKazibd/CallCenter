import { Call } from './call.model';

export interface CallEvent {
  id: number; 
  callId: number; 
  call?: Call; 
  eventType: string; 
  details?: string | null; 
  timestamp: string; 
}