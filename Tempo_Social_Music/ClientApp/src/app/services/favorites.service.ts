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
  getJamsList(userName: string) {
    return this.http.get<Favorites[]>(`${this.apiUri}/getJam/${userName}`)
  }

  //Add an artist to favorites list
  addJam(newFav: Favorites, userName: string) {
    return this.http.post<Favorites>(`${this.apiUri}/addjam/`, { "Favorite": newFav.Favorite, "UserID": newFav.UserId, "SpotTrack": newFav.SpotTrack, "SpotArtist": newFav.SpotArtist })
  }

  //Delete an artist from your favorites list based on the artists ID.
  deleteJam(favorite: string) {
   return this.http.delete(`${this.apiUri}/deleteJam/${favorite}`);
  }
}
