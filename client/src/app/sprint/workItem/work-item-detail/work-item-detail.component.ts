import { Component,  EventEmitter,  Input, OnInit, Output } from '@angular/core';
import { MessageService } from 'primeng/api';
import { error } from 'protractor';
import { SprintService } from 'src/app/_services/sprint.service';
import { WorkItem } from '../../_models/workItem';
import { WorkItemState } from '../../_models/WorkItemState';

@Component({
  selector: 'app-work-item-detail',
  templateUrl: './work-item-detail.component.html',
  styleUrls: ['./work-item-detail.component.scss']
})
export class WorkItemDetailComponent implements OnInit {

  @Input() workItem: WorkItem;  
  @Output() deleted = new EventEmitter();
  display: boolean = false;
  state = WorkItemState;

  constructor(private sprintService: SprintService,private messageService: MessageService) { }

  ngOnInit(): void {
  }

  edit(){
    this.display = true;
  }

  deleteItem(){
    this.sprintService.deleteWorkItem(this.workItem.id).subscribe(
      result => {        
        this.messageService.add({severity:'success', summary:'WorkItem Deleted', detail:'WorkItem Deleted Successfully!'});
        this.deleted.emit(true);
      },
      error =>{
        console.error(error);
        this.messageService.add({ severity: 'error', summary: error.statusText, detail: error.message });
      }
    )
  }

  itemEdited(event:any){
    this.display = false;
  }

}
