import { Component, Input, OnInit, Output,EventEmitter } from '@angular/core';
import { MessageService } from 'primeng/api';
import { SprintService } from 'src/app/_services/sprint.service';
import { WorkItem } from '../../_models/workItem';

@Component({
  selector: 'app-work-item-edit',
  templateUrl: './work-item-edit.component.html',
  styleUrls: ['./work-item-edit.component.scss']
})
export class WorkItemEditComponent implements OnInit {

  @Input() workItem: WorkItem;
  @Input() isEdit: boolean;
  @Input() iterationId: string;
  @Output() newItem:EventEmitter<WorkItem> = new EventEmitter<WorkItem>();  
  @Output() editItem:EventEmitter<boolean> = new EventEmitter<boolean>();   
  submitted: boolean;
  display: boolean;

  constructor(private sprintService: SprintService,private messageService: MessageService) { }

  ngOnInit(): void {
    if(!this.isEdit){
      
    }
  }


  hideDialog(){
    this.display = false;
  }

  save(){    
    if(!this.isEdit){
      this.sprintService.addWorkItem(this.iterationId, this.workItem).subscribe(
        result=>{                  
          this.newItem.emit(result);
        },
        error =>{
          this.messageService.add({ severity: 'error', summary: error.statusText, detail: error.message });
          console.error(error);
        }
      );
    }else if(this.isEdit){
      this.sprintService.updateWorkItem(this.iterationId, this.workItem).subscribe(
        result=>{                            
          this.editItem.emit(true);
        },
        error =>{
          this.messageService.add({ severity: 'error', summary: error.statusText, detail: error.message });
          console.error(error);
        }
      );
    }

  }
}
