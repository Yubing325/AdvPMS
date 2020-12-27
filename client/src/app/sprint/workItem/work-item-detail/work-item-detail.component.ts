import { Component, Input, OnInit } from '@angular/core';
import { WorkItem } from '../../_models/workItem';

@Component({
  selector: 'app-work-item-detail',
  templateUrl: './work-item-detail.component.html',
  styleUrls: ['./work-item-detail.component.scss']
})
export class WorkItemDetailComponent implements OnInit {

  @Input() workItem: WorkItem;
  constructor() { }

  ngOnInit(): void {
  }

}
