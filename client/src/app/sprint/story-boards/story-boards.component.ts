import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SprintService } from 'src/app/_services/sprint.service';
import { WorkItem } from '../_models/workItem';
import {MessageService} from 'primeng/api';

@Component({
  selector: 'app-story-boards',
  templateUrl: './story-boards.component.html',
  styleUrls: ['./story-boards.component.scss'],
  providers: [MessageService]
})
export class StoryBoardsComponent implements OnInit {

  workItems: WorkItem[] = [];
  newWorkItems: WorkItem[] = [];
  activeWorkItems: WorkItem[] = [];
  draggedItem: WorkItem;

  constructor(private sprintService: SprintService, private route: ActivatedRoute,
              private messageService: MessageService) { }

  ngOnInit(): void {
    let iterationId = this.route.snapshot.paramMap.get('id');
    this.sprintService.getWorkItemsByIteration(iterationId).subscribe(
      (result: WorkItem[]) => {
        console.log(result);
        this.workItems = result;
        this.newWorkItems = this.workItems.filter(t=>t.state==0);
        this.activeWorkItems = this.workItems.filter(t=>t.state==1);
      },
      error => {
        this.messageService.add({severity:'error', summary: error.statusText, detail: error.message });
        console.error(error);
      }
    );    

  }

  dragStart(workItem: WorkItem):void{
    this.draggedItem = workItem;
  }

  drop():void{
    this.activeWorkItems.push(this.draggedItem);    
    this.newWorkItems = this.newWorkItems.filter(t=>t.id != this.draggedItem.id);
    console.log(this.newWorkItems);
    //call api to update
  }

}
