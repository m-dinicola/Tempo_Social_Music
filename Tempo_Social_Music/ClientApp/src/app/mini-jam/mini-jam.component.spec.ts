import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MiniJamComponent } from './mini-jam.component';

describe('MiniJamComponent', () => {
  let component: MiniJamComponent;
  let fixture: ComponentFixture<MiniJamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MiniJamComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MiniJamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
