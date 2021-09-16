import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Artists } from '../models/artist';
import { Song } from '../models/song';

@Injectable({
  providedIn: 'root'
})
export class SpotifyapiService {

  apiUri: string = "";

  constructor(private http: HttpClient) {

    console.log(this.apiUri);
  }

  ////Find a Music by artist
  getArtistSearch(artistName: string) {
    return this.http.get<Artists[]>(`${this.apiUri}/getArtistSearch/${artistName}`);
  }

  getSongSearch(songName: string) {
    return this.http.get<Song>(`${this.apiUri}/getSongSearch/${songName}`);
  }

}
