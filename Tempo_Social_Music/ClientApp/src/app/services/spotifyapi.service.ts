import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SpotifyapiService {

  apiUri: string = "";

  constructor(private http: HttpClient) {

    console.log(this.apiUri);
  }



}
