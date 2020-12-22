import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SprintService } from 'src/app/_services/sprint.service';
import { WorkItem } from '../_models/workItem';

@Component({
  selector: 'app-story-boards',
  templateUrl: './story-boards.component.html',
  styleUrls: ['./story-boards.component.scss']
})
export class StoryBoardsComponent implements OnInit {

  workItems: WorkItem[] = [];

  constructor(private sprintService: SprintService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let iterationId = this.route.snapshot.paramMap.get('id');
    this.sprintService.getWorkItemsByIteration(iterationId).subscribe(
      (result: WorkItem[]) => {
        console.log(result);
        this.workItems = result;
      },
      error => {
        console.error(error);
      }
    );
  }

}
