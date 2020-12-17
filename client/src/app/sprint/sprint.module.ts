import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoryBoardsComponent } from './story-boards/story-boards.component';
import { BacklogsComponent } from './backlogs/backlogs.component';
import { IterationEditComponent } from './iteration/iteration-edit/iteration-edit.component';



@NgModule({
  declarations: [ StoryBoardsComponent, BacklogsComponent, IterationEditComponent],
  imports: [
    CommonModule
  ]
})
export class SprintModule { }
