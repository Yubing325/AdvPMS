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
  }

}
