//modules
import {CheckboxModule} from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {TableModule} from 'primeng/table';

//components
import { StoryBoardsComponent } from './story-boards/story-boards.component';
import { BacklogsComponent } from './backlogs/backlogs.component';
import { IterationEditComponent } from './iteration/iteration-edit/iteration-edit.component';
import { IterationGridComponent } from './iteration/iteration-grid/iteration-grid.component';
import { SprintRoutingModule } from './sprint-routing.module.';






@NgModule({
  declarations: [ StoryBoardsComponent, BacklogsComponent, IterationEditComponent, IterationGridComponent],
  imports: [
    CommonModule,
    SprintRoutingModule,
    FormsModule,    
    TableModule
  ]
})
export class SprintModule { }
