import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TempoUser } from '../models/TempoUser';
import { SpotifyapiService } from '../services/spotifyapi.service';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(private tempoDBService: TempoDBAPIService, private spotifyService: SpotifyapiService, route: ActivatedRoute) { }

  userSearch: TempoUser[] = [];
  musicSearch = Artist[];

  ngOnInit(): void {
  }


  getUserSearch(userName: string) {
  this.tempoDBService.getUserSearch(userName).subscribe(
    result => {
      this.userSearch = result;
      console.log(this.userSearch);
    },
    error => console.log(error)
  );
  }

  getMusicSearch(keyword: string) {
    this.spotifyService.getMusicSearch(keyword).subscribe(
      result => {
        this.musicSearch = result;
        console.log(this.musicSearch);
      },
      error => console.log(error)
    );
  }
}


