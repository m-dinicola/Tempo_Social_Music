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

  //Find a user by userName
  getUserByName(userName: string) {
    return this.http.get<TempoUser[]>(`${this.apiUri}/username/${userName}`);
  }

  //Add a found user. Link this to a button.
  addUserFriend(user1: string, user2: string) {
    var message = `${user1}&${user2}`
    return this.http.post<TempoUser>(`${this.apiUri}/addUserFriend/${message}`, {});
  }

  //Delete a user by the userName.
  deleteUserFriend(user1: string, user2: string) {
    var message = `${user1}&${user2}`
    return this.http.delete(`${this.apiUri}/deleteUserFriend/${message}`);
  }

  //Create a new user. Link this to ngForm.
  createUser() {
    return this.http.post<TempoUser>(`${this.apiUri}/user`, {});
  }
}
