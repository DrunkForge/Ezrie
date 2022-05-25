import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { IdentityServiceComponent } from './identity-service.component';

describe('IdentityServiceComponent', () => {
  let component: IdentityServiceComponent;
  let fixture: ComponentFixture<IdentityServiceComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ IdentityServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IdentityServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
