import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
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
  submitted: boolean;

  constructor() { }

  ngOnInit(): void {
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
    this.workItem = {
      title:"",
      description:"",
      created:null,
      id:null,
      iteration: null,
      iterationId:null,
      lastModified:null,
      priority:0,
      state:0 
    };
  }

  add(){

  }

  openNew(){

    this.submitted = false;
    this.display=true;
  }

  hideDialog(){
    this.display = false;
  }

  save(){
    
  }

}
