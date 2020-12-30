import { Component,  Input, OnInit, Output } from '@angular/core';
import { WorkItem } from '../../_models/workItem';

@Component({
  selector: 'app-work-item-detail',
  templateUrl: './work-item-detail.component.html',
  styleUrls: ['./work-item-detail.component.scss']
})
export class WorkItemDetailComponent implements OnInit {

  @Input() workItem: WorkItem;  
  display: boolean = false;
  constructor() { }

  ngOnInit(): void {
  }

  edit(){
    this.display = true;
  }

  itemEdited(event:any){
    this.display = false;
  }

}
