import { Injectable, Service } from '@angular/core';
import { delay, Observable, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
    searchCustomer(query: string): Observable<any> {
        const lowerCaseQuery = query.toLowerCase().trim();
        
        const customer = this.mockCustomers.find(c => 
        c.phone.includes(lowerCaseQuery) || 
        c.name.toLowerCase().includes(lowerCaseQuery)
        );
        
        return of(customer || null).pipe(delay(500)); 
    }
  private mockCustomers = [
    {
      phone: '01711223344',
      name: 'Masum Kazi',
      email: 'masum@example.com',
      address: 'Dhaka, Bangladesh',
      totalSpent: 12500,
      purchaseHistory: [
        { date: '2026-07-10', item: 'Wireless Headphone', amount: 2500, status: 'Delivered' },
        { date: '2026-07-20', item: 'Mechanical Keyboard', amount: 5000, status: 'Shipped' }
      ]
    },
    {
      phone: '01811223344',
      name: 'John Doe',
      email: 'john@example.com',
      address: 'Chittagong, Bangladesh',
      totalSpent: 1500,
      purchaseHistory: [
        { date: '2026-06-15', item: 'Gaming Mouse', amount: 1500, status: 'Delivered' }
      ]
    }
  ];
}