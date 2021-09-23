import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Artists } from '../models/artists';
import { Song } from '../models/song';

@Injectable({
  providedIn: 'root'
})
export class SpotifyapiService {

  apiUri: string = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUri: string) {
    this.apiUri = `${baseUri}api/spotify`;
    console.log(this.apiUri);
  }

  //Find music by artist
  getArtistByName(artistName: string) {
    return this.http.get<Artists>(`${this.apiUri}/getArtistByName/${artistName}`);
  }

  //Find music by song
  getSongByName(songName: string) {
    return this.http.get<Song>(`${this.apiUri}/getSongByName/${songName}`);
  }

  //Find an arist by an ID
  getArtistById(artistId: string) {
    return this.http.get<Artists>(`${this.apiUri}/getArtistById/${artistId}`);
  }

  //Find a song by an ID
  getSongById(songId: string) {
    return this.http.get<Song>(`${this.apiUri}/getSongById/${songId}`);
  }
}
