import { Component, Input, OnInit, Output,EventEmitter } from '@angular/core';
import {  Router } from '@angular/router';

import { MenuItem, MessageService } from 'primeng/api';
import { SprintService } from 'src/app/_services/sprint.service';
import { Iteration } from '../../_models/iteration';
import { WorkItem } from '../../_models/workItem';



@Component({
  selector: 'app-sb-toolbar',
  templateUrl: './sb-toolbar.component.html',
  styleUrls: ['./sb-toolbar.component.scss']
})
export class SbToolbarComponent implements OnInit {

  items: MenuItem[];
  iterations: Iteration[] = [];
  selectedIteration: Iteration;
  workItem: WorkItem;
  display:boolean;  
  @Input() iterationId: string;
  @Output() newItem:EventEmitter<WorkItem> = new EventEmitter<WorkItem>();  

  constructor(private sprintService: SprintService,private messageService: MessageService,
              private router: Router) { 
    this.sprintService.getIterations().subscribe
    (
      (results:Iteration[]) => {
        this.iterations = results;
        this.selectedIteration = this.iterations.find(t=>t.id == this.iterationId);
      },
      error => {
        this.messageService.add({severity:'error', summary: error.statusText, detail: error.message });
        console.error(error);
      }
    );   
  }

  ngOnInit(): void {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false; //disable route to allow same url rediercting
    this.items = [
      {label: 'Add', icon: 'pi pi-refresh', command: () => {
          this.add();
      }},
      // {label: 'Delete', icon: 'pi pi-times', command: () => {
      //     this.delete();
      // }},
      {label: 'Angular.io', icon: 'pi pi-info', url: 'http://angular.io'},
      {separator:true},
      {label: 'Setup', icon: 'pi pi-cog', routerLink: ['/setup']}
    ];
    this.initWorkItem();
  }

  private initWorkItem() {
    this.workItem = {
      title: "",
      description: "",
      created: null,
      id: null,
      iteration: null,
      iterationId: null,
      lastModified: null,
      priority: 0,
      state: 0
    };
  }

  add(){
    
  }

  selectIteration(iteration: Iteration){    
    this.router.navigate(["/sprints/story-boards/", iteration.id]);
  }

  openNew(){
    this.display=true;
    this.initWorkItem();
  }

  itemAdded(){
    this.display = false;    
    this.newItem.emit(this.workItem);
  }  

}
