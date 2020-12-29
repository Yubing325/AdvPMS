import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SprintService } from 'src/app/_services/sprint.service';
import { WorkItem } from '../_models/workItem';
import { MessageService} from 'primeng/api';

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
  resolvedWorkItems: WorkItem[] = [];
  closedWorkItems: WorkItem[] = [];
  draggedItem: WorkItem;
  iterationId: string;

  constructor(private sprintService: SprintService, private route: ActivatedRoute,
              private messageService: MessageService) { }

  ngOnInit(): void {
    this.iterationId = this.route.snapshot.paramMap.get('id');
    this.refreshWorkItems(this.iterationId); 
  }

  private refreshWorkItems(iterationId: string) {
    this.sprintService.getWorkItemsByIteration(iterationId).subscribe(
      (result: WorkItem[]) => {
        console.log(result);
        this.workItems = result;
        this.newWorkItems = this.workItems.filter(t => t.state == 0);
        this.activeWorkItems = this.workItems.filter(t => t.state == 1);
        this.resolvedWorkItems = this.workItems.filter(t => t.state == 2);
        this.closedWorkItems = this.workItems.filter(t => t.state == 3);
      },
      error => {
        this.messageService.add({ severity: 'error', summary: error.statusText, detail: error.message });
        console.error(error);
      }
    );
  }

  dragStart(workItem: WorkItem):void{
    this.draggedItem = workItem;
  }

  drop(state:number){
    this.alterDropColumns(state);
    this.alterDragColumns(this.draggedItem);
    this.updateWIState(state); 
    this.draggedItem = null;
  }

  private alterDropColumns(state: number) {
    switch (state) {
      case 0: //dropped to "New"
        this.newWorkItems.push(this.draggedItem);
        break;
      case 1: //dropped to "Active" 
        this.activeWorkItems.push(this.draggedItem);
        break;
      case 2: //dropped to "Resolved"
        this.resolvedWorkItems.push(this.draggedItem);
        break;
      case 3: //dropped to "Closed" 
        this.closedWorkItems.push(this.draggedItem);
        break;
      default:
        this.messageService.add({severity:'error', summary: "invalid state", detail: "drop work item state is invalid"});
    }
  }

  private updateWIState(state: number) {
    this.sprintService.updateWorkItemState(this.draggedItem.id, state).subscribe(
      result => {
        this.refreshWorkItems(this.iterationId);
      },
      error => {
        this.messageService.add({ severity: 'error', summary: error.statusText, detail: error.message });
        console.error(error);
      }
    );
  }

  private alterDragColumns(draggedItem: WorkItem):void{
    switch(draggedItem.state){
      case 0: //dragged from "New" 
        this.newWorkItems = this.newWorkItems.filter(t=>t.id != this.draggedItem.id);
        break;
      case 1: //dragged from "Active" 
        this.activeWorkItems = this.activeWorkItems.filter(t=>t.id != this.draggedItem.id);
        break;
      case 2: //dragged from "Resolved" 
        this.resolvedWorkItems = this.resolvedWorkItems.filter(t=>t.id != this.draggedItem.id);
        break;
      case 3: //dragged from "Closed" 
        this.closedWorkItems = this.closedWorkItems.filter(t=>t.id != this.draggedItem.id);
        break;
    
      default:
        this.messageService.add({severity:'error', summary: "invalid state", detail: "drag work item state is invalid"});
    }
  }

  itemAdded(event:any){
    this.refreshWorkItems(this.iterationId);
  }

}
