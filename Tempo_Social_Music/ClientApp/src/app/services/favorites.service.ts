import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Artists } from '../models/Artists';
import { Favorites } from '../models/Favorites';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {

  apiUri: string = "https://localhost:44346/api/tempodb";
  jamsList: Favorites;

  constructor(private http: HttpClient) {

    console.log(this.apiUri);
  }

  //get favorites tempodb/jams/userPK
  getJamsList(userPk: number) {
    return this.http.get<Favorites[]>(`${this.apiUri}/Jams/${userPk}`)
  }

  //Add an artist to favorites list
  addJam(newFav: Favorites) {
    return this.http.post<Favorites>(`${this.apiUri}/Jams/`, newFav)
  }

  //Delete an artist from your favorites list based on the artists ID.
  deleteJam(favorite: Favorites) {
    return this.http.delete(`${this.apiUri}/Jams/${favorite.Favorite}`);
  }
}
