import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class TenantServiceService {
  apiName = 'TenantService';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/TenantService/sample' },
      { apiName: this.apiName }
    );
  }
}
