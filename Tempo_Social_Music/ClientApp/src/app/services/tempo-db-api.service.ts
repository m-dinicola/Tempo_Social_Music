import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { TempoUser } from '../models/TempoUser';

@Injectable({
  providedIn: 'root'
})
export class TempoDBAPIService {

  apiUri: string = "";
  newDataAdded = new EventEmitter<string>();

  constructor(private http: HttpClient) {

    console.log(this.apiUri)
  }

  getUserSearch(userName: string) {
    return this.http.get<TempoUser[]>(`${this.apiUri}/getUserSearch/${userName}`);
  }

  

}
