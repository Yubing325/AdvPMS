import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SprintService } from 'src/app/_services/sprint.service';
import { Iteration } from '../../_models/iteration';

@Component({
  selector: 'app-iteration-grid',
  templateUrl: './iteration-grid.component.html',
  styleUrls: ['./iteration-grid.component.scss']
})
export class IterationGridComponent implements OnInit {

  iterations: Iteration[] = [];  
 
 

  constructor(private sprintService: SprintService, private router: Router) { }

  ngOnInit(): void {
    this.sprintService.getIterations().subscribe
    (
      (results:Iteration[]) => {
        this.iterations = results;
      },
      error => {
        console.error(error);
      }
    );   
  }

  onRowSelect(event: any){
    console.log(event);
    this.router.navigate(['/sprints/story-boards', event.data.id]);
  }

}
