import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { TenantServiceComponent } from './components/tenant-service.component';
import { TenantServiceRoutingModule } from './tenant-service-routing.module';

@NgModule({
  declarations: [TenantServiceComponent],
  imports: [CoreModule, ThemeSharedModule, TenantServiceRoutingModule],
  exports: [TenantServiceComponent],
})
export class TenantServiceModule {
  static forChild(): ModuleWithProviders<TenantServiceModule> {
    return {
      ngModule: TenantServiceModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<TenantServiceModule> {
    return new LazyModuleFactory(TenantServiceModule.forChild());
  }
}
