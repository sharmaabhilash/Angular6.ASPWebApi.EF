import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GrantleaveComponent } from './grantleave.component';

describe('GrantleaveComponent', () => {
  let component: GrantleaveComponent;
  let fixture: ComponentFixture<GrantleaveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GrantleaveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GrantleaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
