import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IterationGridComponent } from './iteration-grid.component';

describe('IterationGridComponent', () => {
  let component: IterationGridComponent;
  let fixture: ComponentFixture<IterationGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IterationGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IterationGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
