import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Artists } from '../models/Artists';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {

  apiUri: string = "https://localhost:44436/api/spotify";

  constructor(private http: HttpClient) {

    console.log(this.apiUri);
  }

  //Add an artist to favorites list
  addArtistFavorite(artistId: Artists) {
    return this.http.post<Artists>(`${this.apiUri}/addArtistFavorite/${artistId}`, { "artistId": artistId.artistId });
  }
}
