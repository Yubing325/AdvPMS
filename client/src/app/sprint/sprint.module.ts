//modules
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {TableModule} from 'primeng/table';
import {ButtonModule} from 'primeng/button';
import {ToastModule} from 'primeng/toast';
import {PanelModule} from 'primeng/panel';
import {DragDropModule} from 'primeng/dragdrop';

//components
import { StoryBoardsComponent } from './story-boards/story-boards.component';
import { BacklogsComponent } from './backlogs/backlogs.component';
import { IterationEditComponent } from './iteration/iteration-edit/iteration-edit.component';
import { IterationGridComponent } from './iteration/iteration-grid/iteration-grid.component';
import { SprintRoutingModule } from './sprint-routing.module.';
import { WorkItemGridComponent } from './workItem/work-item-grid/work-item-grid.component';
import { WorkItemEditComponent } from './workItem/work-item-edit/work-item-edit.component';
import { WorkItemDetailComponent } from './workItem/work-item-detail/work-item-detail.component';






@NgModule({
  declarations: [ StoryBoardsComponent, BacklogsComponent, IterationEditComponent, IterationGridComponent, WorkItemGridComponent, WorkItemEditComponent, WorkItemDetailComponent],
  imports: [
    CommonModule,
    SprintRoutingModule,
    FormsModule,    
    TableModule,
    ButtonModule,
    ToastModule,
    PanelModule,
    DragDropModule
  ]
})
export class SprintModule { }
