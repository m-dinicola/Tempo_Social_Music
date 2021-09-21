import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Artists } from '../models/Artists';
import { Favorites } from '../models/Favorites';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {

  apiUri: string = "https://localhost:44346/api/spotify";
  jamsList: Favorites;

  constructor(private http: HttpClient) {

    console.log(this.apiUri);
  }

  //get favorites tempodb/jams/userPK
  getJamsList(userPK: number) {
    return this.http.get<Favorites[]>(`${this.apiUri}/getJam/${userPK}`)
  }

  //Add an artist to favorites list
  addJam(newFav: Favorites) {
    return this.http.post<Favorites>(`${this.apiUri}/addjam/`, { "Favorite": newFav.Favorite, "UserID": newFav.UserId, "SpotTrack": newFav.SpotTrack, "SpotArtist": newFav.SpotArtist })
  }

  //Delete an artist from your favorites list based on the artists ID.
  deleteJam(favorite: string) {
   return this.http.delete(`${this.apiUri}/deleteJam/${favorite}`);
  }
}
