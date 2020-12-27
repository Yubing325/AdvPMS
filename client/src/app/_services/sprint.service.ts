import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Iteration } from '../sprint/_models/iteration';
import { WorkItem } from '../sprint/_models/workItem';

@Injectable({
  providedIn: 'root'
})
export class SprintService {

baseUrl: string = environment.apiEndpoint;


constructor(private http: HttpClient) { }

getIterations() : Observable<Iteration[]>{
  return this.http.get<Iteration[]>(this.baseUrl + '/iterations');
}

getAllWorkItems(): Observable<WorkItem[]>{
  return this.http.get<WorkItem[]>(this.baseUrl+ '/workitems');
}

getWorkItemsByIteration(iterationId: string){
  return this.http.get<WorkItem[]>(this.baseUrl +  '/iterations/' + iterationId + '/workitems');
}

updateWorkItemState(workItemId:string, state:number){
  return this.http.put(this.baseUrl + '/workitems/'+ workItemId +'/'+ state, null);
}

}
