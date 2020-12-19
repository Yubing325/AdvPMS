import { Component, OnInit } from '@angular/core';
import { SprintService } from 'src/app/_services/sprint.service';
import { WorkItem } from '../../_models/workItem';

@Component({
  selector: 'app-work-item-grid',
  templateUrl: './work-item-grid.component.html',
  styleUrls: ['./work-item-grid.component.scss']
})
export class WorkItemGridComponent implements OnInit {

  workItems: WorkItem[] = [];

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
  }

}
