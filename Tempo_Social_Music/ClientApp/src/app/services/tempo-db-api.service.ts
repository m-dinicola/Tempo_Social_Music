import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { EventEmitter, Inject, Injectable } from '@angular/core';
import { TempoUser } from '../models/TempoUser';
import { Connection } from '../models/Connection';

@Injectable({
  providedIn: 'root'
})
export class TempoDBAPIService {

  apiUri: string = "";
  newDataAdded = new EventEmitter<string>();

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUri: string) {
    this.apiUri = `${baseUri}api/tempodb`;
    console.log(this.apiUri);
  }

  //get active user
  getActiveUser() {
    return this.http.get<TempoUser>(`${this.apiUri}/user`);
  }

  //Find a user by userName
  getUserByName(userName: string) {
    return this.http.get<TempoUser>(`${this.apiUri}/username/${userName}`);
  }

  //Add a found user. Link this to a button.
  addUserFriend(userConnect: string) {
    var message = `${userConnect}`
    return this.http.post<TempoUser>(`${this.apiUri}/addUserFriend/${message}`, {});
  }

  //Delete a user by the userName.
  deleteUserFriend(user1: string, user2: string) {
    var message = `${user1}&${user2}`
    return this.http.delete(`${this.apiUri}/deleteUserFriend/${message}`);
  }

  //Get a list of friends for a given user.
  getFriends(userNum: number) {
    return this.http.get<Connection[]>(`${this.apiUri}/connections/${userNum}`);
  }

  //Create a new user. Link this to ngForm.
  createUser(newUser: TempoUser) {
    return this.http.post<TempoUser>(`${this.apiUri}/user`, newUser);
  }

  //Update user bio
  updateBio(newBio: string) {
    return this.http.put<any>(`${this.apiUri}/user`, newBio);

  }

  getUserBio(bio: string) {
    return this.http.get<TempoUser>(`${this.apiUri}/user`)
  }

  updateProfile(newInfo: TempoUser) {
    return this.http.put<TempoUser>(`${this.apiUri}/user`, newInfo);
  }
}
