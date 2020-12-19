import { Component, OnInit } from '@angular/core';
import { SprintService } from 'src/app/_services/sprint.service';
import { Iteration } from '../../_models/iteration';

@Component({
  selector: 'app-iteration-grid',
  templateUrl: './iteration-grid.component.html',
  styleUrls: ['./iteration-grid.component.scss']
})
export class IterationGridComponent implements OnInit {

  iterations: Iteration[] = [];  
  cols: { field: string; header: string; }[];
  cols2 = ["id", "title", "date"];

  constructor(private sprintService: SprintService) { }

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

    this.cols = [
      { field: 'id', header: 'Id' },
      { field: 'title', header: 'Title' },
      { field: 'created', header: 'Created' }
    ];
  }

}
