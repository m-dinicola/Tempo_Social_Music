import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TempoUserComponent } from './tempo-user.component';

describe('TempoUserComponent', () => {
  let component: TempoUserComponent;
  let fixture: ComponentFixture<TempoUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TempoUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TempoUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
