import { Service, signal } from '@angular/core';

@Service()
export class AuthService {
    currentAgentId = signal<number | null>(null);
    
    setAgent(id: number) {
    this.currentAgentId.set(id);
  }
}