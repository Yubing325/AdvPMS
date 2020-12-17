import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StoryBoardsComponent } from './story-boards.component';

describe('StoryBoardsComponent', () => {
  let component: StoryBoardsComponent;
  let fixture: ComponentFixture<StoryBoardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StoryBoardsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StoryBoardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
