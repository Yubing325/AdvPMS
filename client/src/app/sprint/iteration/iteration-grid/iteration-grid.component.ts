import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { SprintService } from 'src/app/_services/sprint.service';
import { Iteration } from '../../_models/iteration';

@Component({
  selector: 'app-iteration-grid',
  templateUrl: './iteration-grid.component.html',
  styleUrls: ['./iteration-grid.component.scss'],
  providers:[MessageService]
})
export class IterationGridComponent implements OnInit {

  iterations: Iteration[] = [];  
 
 

  constructor(private sprintService: SprintService, private router: Router, 
            private messageService: MessageService) { }

  ngOnInit(): void {
    this.sprintService.getIterations().subscribe
    (
      (results:Iteration[]) => {
        this.iterations = results;
      },
      error => {
        this.messageService.add({severity:'error', summary: error.statusText, detail: error.message });
        console.error(error);
      }
    );   
  }

  onRowSelect(event: any){    
    this.router.navigate(['/sprints/story-boards', event.data.id]);
  }

}
