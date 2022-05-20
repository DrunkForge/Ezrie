import { ModuleWithProviders, NgModule } from '@angular/core';
import { TENANT_SERVICE_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class TenantServiceConfigModule {
  static forRoot(): ModuleWithProviders<TenantServiceConfigModule> {
    return {
      ngModule: TenantServiceConfigModule,
      providers: [TENANT_SERVICE_ROUTE_PROVIDERS],
    };
  }
}
