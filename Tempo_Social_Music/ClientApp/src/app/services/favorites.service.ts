import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Artists } from '../models/Artists';
import { Favorites } from '../models/Favorites';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {

  apiUri: string;
  jamsList: Favorites;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUri: string) {
    this.apiUri = `${baseUri}api/tempodb`;

    console.log(this.apiUri);
  }

  //get favorites tempodb/jams/userPK
  getJams(userPk: number) {
    return this.http.get<Favorites[]>(`${this.apiUri}/Jams/${userPk}`)
  }

  //Add an artist to favorites list
  addJam(newFav: Favorites) {
    return this.http.post<Favorites>(`${this.apiUri}/Jams/`, newFav)
  }

  //Delete an artist from your favorites list based on the artists ID.
  deleteJam(favorite: number) {
    return this.http.delete(`${this.apiUri}/Jams/${favorite}`);
  }
}
