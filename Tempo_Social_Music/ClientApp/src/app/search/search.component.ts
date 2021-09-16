import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TempoUser } from '../models/TempoUser';
import { Artists } from '../models/Artists';
import { SpotifyapiService } from '../services/spotifyapi.service';
import { TempoDBAPIService } from '../services/tempo-db-api.service';
import { Song } from '../models/Song';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(private tempoDBService: TempoDBAPIService, private spotifyService: SpotifyapiService, route: ActivatedRoute) { }

  keyword: string;
  userSearch: TempoUser[] = [];
  artistSearch: Artists[] = [];
  songSearch: Song;

  ngOnInit(): void {
  }

  //Find a user by user name
  getUserSearch(userName: string) {
  this.tempoDBService.getUserSearch(userName).subscribe(
    result => {
      this.userSearch = result;
      console.log(this.userSearch);
    },
    error => console.log(error)
  );
  }

  //Find an artist/band by a keyword
  getArtistSearch(keyword: string) {
    this.spotifyService.getArtistSearch(keyword).subscribe(
      result => {
        this.artistSearch = result;
        console.log(this.artistSearch);
      },
      error => console.log(error)
    );
  }

  //Find a song by a keyword
  getSongSearch(keyword: string) {
    this.spotifyService.getSongSearch(keyword).subscribe(
      result => {
        this.songSearch = result;
        console.log(this.songSearch);
      },
      error => console.log(error)
    );
  }

  //This will utilize the search form. It will take in a string "keyword" and seach
  //for that keyword when the search button is clicked.
  onSubmit(form: NgForm) {
    this.keyword = form.form.value;
    console.log(this.keyword);
    this.getUserSearch(this.keyword);
  }
}


