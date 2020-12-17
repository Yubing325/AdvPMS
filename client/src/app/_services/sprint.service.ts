import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Iteration } from '../sprint/_models/iteration';

@Injectable({
  providedIn: 'root'
})
export class SprintService {

baseUrl: string = environment.apiEndpoint;


constructor(private http: HttpClient) { }

getIterations() : Observable<Iteration[]>{
  return this.http.get<Iteration[]>(this.baseUrl + '/iterations');
}

}
