import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { QueueService } from '../../services/queue.service';
import { HttpClient } from '@microsoft/signalr';
import { SimulationService } from '../../services/simulation.service';

@Component({
  selector: 'app-call-simulator.component',
  imports: [FormsModule],
  templateUrl: './call-simulator.component.html',
  styleUrl: './call-simulator.component.css',
})
export class CallSimulatorComponent implements OnInit {
  private queueService = inject(QueueService); 
  private simulationService = inject(SimulationService);
  queues = signal<any[]>([]); 
  phoneNumber = '';
  selectedQueueId = '';

  ngOnInit() {
    this.loadQueues();
  }

  loadQueues() { 
    this.queueService.getQueues().subscribe({
      next: (res: any) => this.queues.set(res),
      error: (err) => console.error('❌ Failed to load queues', err)
    });
  }

  generateCall() {
    const callData = {
      customerPhoneNumber: this.phoneNumber,
      queueId: this.selectedQueueId
    };
 
    this.simulationService.generateCall(callData).subscribe({
        next: () => {
          console.log('✅ Call pushed to backend successfully!'); 
          this.phoneNumber = '';
          this.selectedQueueId = '';
        },
        error: (err: any) => console.error('❌ Error generating call', err)
      });
  }
}