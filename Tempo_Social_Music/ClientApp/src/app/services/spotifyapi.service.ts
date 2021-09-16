import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Artists } from '../models/artists';
import { Song } from '../models/song';

@Injectable({
  providedIn: 'root'
})
export class SpotifyapiService {

  apiUri: string = "";

  constructor(private http: HttpClient) {

    console.log(this.apiUri);
  }

  //Find music by artist
  getArtistSearch(artistName: string) {
    return this.http.get<Artists[]>(`${this.apiUri}/getArtistSearch/${artistName}`);
  }

  //Find music by song
  getSongSearch(songName: string) {
    return this.http.get<Song>(`${this.apiUri}/getSongSearch/${songName}`);
  }

  //Add an artist to favorites list
  addArtistFavorite(artistId: Artists) {
    return this.http.post<Artists>(`${this.apiUri}/addArtistFavorite/${artistId}`, { "artistId": artistId.artistId });
  }
}
