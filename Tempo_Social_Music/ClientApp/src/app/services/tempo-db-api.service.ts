import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { TempoUser } from '../models/TempoUser';

@Injectable({
  providedIn: 'root'
})
export class TempoDBAPIService {

  apiUri: string = "https://localhost:44346/api/tempodb";
  newDataAdded = new EventEmitter<string>();

  constructor(private http: HttpClient) {

    console.log(this.apiUri)
  }

  //Find a user by User Name
  getUserSearch(userName: string) {
    return this.http.get<TempoUser[]>(`${this.apiUri}/getUserSearch/${userName}`);
  }

  //Add a found user. Link this to a button.
  addUserFriend(userPK: number) {
    return this.http.post<TempoUser>(`${this.apiUri}/addUserFriend/${userPK}`, { "userPK": userPK });
  }

  //Delete a user by the user's ID. Called userPK in the database.
  deleteUserFriend(userPK: number) {
    return this.http.delete(`${this.apiUri}/deleteUserFriend/${userPK}`);
  }

}
