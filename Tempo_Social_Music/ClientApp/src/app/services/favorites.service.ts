import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Artists } from '../models/Artists';
import { Favorites } from '../models/Favorites';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {

  apiUri: string = "https://localhost:44346/api/spotify";

  constructor(private http: HttpClient) {

    console.log(this.apiUri);
  }

  //get favorites tempodb/jams/userPK

  //Add an artist to favorites list
  addJams(newFav: Favorites) {
    return this.http.post<Favorites[]>(`${this.apiUri}/jams/` , newFav);
  }

  //Delete an artist from your favorites list based on the artists ID.
  deleteJams(favorite: number, fav:Favorites) {
    this.http.delete(`${this.apiUri}/jams/${favorite}`);
  }
}
