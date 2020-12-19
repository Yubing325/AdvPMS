import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BacklogsComponent } from './backlogs/backlogs.component';
import { IterationGridComponent } from './iteration/iteration-grid/iteration-grid.component';
import { WorkItemGridComponent } from './workItem/work-item-grid/work-item-grid.component';

const routes: Routes = [
  {
    path:"",
    component: WorkItemGridComponent
  },
  {
    path: "backlogs",
    component: BacklogsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SprintRoutingModule { }
