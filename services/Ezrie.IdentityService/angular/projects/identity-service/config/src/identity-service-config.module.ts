import { ModuleWithProviders, NgModule } from '@angular/core';
import { IDENTITY_SERVICE_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class IdentityServiceConfigModule {
  static forRoot(): ModuleWithProviders<IdentityServiceConfigModule> {
    return {
      ngModule: IdentityServiceConfigModule,
      providers: [IDENTITY_SERVICE_ROUTE_PROVIDERS],
    };
  }
}
