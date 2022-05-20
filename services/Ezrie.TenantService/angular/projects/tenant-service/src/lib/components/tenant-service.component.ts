import { Component, OnInit } from '@angular/core';
import { TenantServiceService } from '../services/tenant-service.service';

@Component({
  selector: 'lib-tenant-service',
  template: ` <p>tenant-service works!</p> `,
  styles: [],
})
export class TenantServiceComponent implements OnInit {
  constructor(private service: TenantServiceService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
