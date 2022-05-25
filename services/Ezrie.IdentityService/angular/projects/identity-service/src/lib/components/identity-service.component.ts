import { Component, OnInit } from '@angular/core';
import { IdentityServiceService } from '../services/identity-service.service';

@Component({
  selector: 'lib-identity-service',
  template: ` <p>identity-service works!</p> `,
  styles: [],
})
export class IdentityServiceComponent implements OnInit {
  constructor(private service: IdentityServiceService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
