import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CallSimulatorComponent } from './call-simulator.component';

describe('CallSimulatorComponent', () => {
  let component: CallSimulatorComponent;
  let fixture: ComponentFixture<CallSimulatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CallSimulatorComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CallSimulatorComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
