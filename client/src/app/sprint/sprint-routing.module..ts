import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BacklogsComponent } from './backlogs/backlogs.component';
import { IterationGridComponent } from './iteration/iteration-grid/iteration-grid.component';
import { StoryBoardsComponent } from './story-boards/story-boards.component';
import { WorkItemGridComponent } from './workItem/work-item-grid/work-item-grid.component';

const routes: Routes = [
  {
    path:"",
    component: WorkItemGridComponent
  },
  {
    path: "backlogs",
    component: BacklogsComponent
  },
  {
    path:"iterations",
    component: IterationGridComponent
  },
  {
    path:"story-boards/:id",
    component: StoryBoardsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SprintRoutingModule { }
