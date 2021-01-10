import { Component, Input, OnInit } from '@angular/core';
import { SprintService } from 'src/app/_services/sprint.service';
import { WorkItem } from '../../_models/workItem';
import { WorkItemState } from "../../_models/WorkItemState";

@Component({
  selector: 'app-work-item-grid',
  templateUrl: './work-item-grid.component.html',
  styleUrls: ['./work-item-grid.component.scss']
})
export class WorkItemGridComponent implements OnInit {

  workItems: WorkItem[] = [];
  state = WorkItemState;
  cols: any[];
  exportColumns: any[];
  _selectedColumns: any[];

  constructor(private sprintService: SprintService) { }

  ngOnInit(): void {
    this.sprintService.getAllWorkItems().subscribe
    (
      (results:WorkItem[]) => {
        this.workItems = results;
      },
      error => {
        console.error(error);
      }
    );
    this.cols = [
      { field: 'title', header: 'Title' },
      { field: 'created', header: 'Created' },
      { field: 'lastModified', header: 'Modifiled' },
      { field: 'iteration', header: 'Iteration' },
    ];
    this._selectedColumns = this.cols;
  }

  get selectedColumns(): any[] {
    return this._selectedColumns;
  }

  set selectedColumns(val: any[]) {
    //restore original order
    this._selectedColumns = this.cols.filter(col => val.includes(col));
  }

}
