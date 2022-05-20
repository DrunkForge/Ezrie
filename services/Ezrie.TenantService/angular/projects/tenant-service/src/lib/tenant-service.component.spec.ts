import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { TenantServiceComponent } from './tenant-service.component';

describe('TenantServiceComponent', () => {
  let component: TenantServiceComponent;
  let fixture: ComponentFixture<TenantServiceComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TenantServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TenantServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
