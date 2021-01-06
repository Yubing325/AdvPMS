import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AddIterationDto } from '../sprint/_models/addIterationDto';
import { AddWorkItemDto } from '../sprint/_models/addWorkItemDto';
import { Iteration } from '../sprint/_models/iteration';
import { WorkItem } from '../sprint/_models/workItem';

@Injectable({
  providedIn: 'root'
})
export class SprintService {

baseUrl: string = environment.apiEndpoint;


constructor(private http: HttpClient) { }

addIteration(model: AddIterationDto):Observable<Iteration> {
  return this.http.post<Iteration>(this.baseUrl + '/iterations', model);
}

deleteIteration(id: string){
  return this.http.delete(this.baseUrl + '/iterations/'+ id);
}

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

addWorkItem(iterationId: string, workItem:AddWorkItemDto): Observable<WorkItem>{
  return this.http.post<WorkItem>(this.baseUrl+"/iterations/"+ iterationId +"/workitems", workItem);
}

updateWorkItem(iterationId: string, workItem:WorkItem){
  return this.http.put(this.baseUrl+"/iterations/"+ iterationId +"/workitems/"+workItem.id, workItem);
}

deleteWorkItem(id:string){
  return this.http.delete(this.baseUrl + "/workitems/"+id);
}

}
