import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { IdentityServiceComponent } from './components/identity-service.component';
import { IdentityServiceRoutingModule } from './identity-service-routing.module';

@NgModule({
  declarations: [IdentityServiceComponent],
  imports: [CoreModule, ThemeSharedModule, IdentityServiceRoutingModule],
  exports: [IdentityServiceComponent],
})
export class IdentityServiceModule {
  static forChild(): ModuleWithProviders<IdentityServiceModule> {
    return {
      ngModule: IdentityServiceModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<IdentityServiceModule> {
    return new LazyModuleFactory(IdentityServiceModule.forChild());
  }
}
