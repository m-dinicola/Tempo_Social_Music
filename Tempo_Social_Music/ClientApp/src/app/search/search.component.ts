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

  songTitle: string;
  userSearch: TempoUser[] = [];
  artistSearch: Artists;
  songSearch: Song;

  ngOnInit(): void {
  }

  //Find a user by user name
  getUserByName(userName: string) {
    this.tempoDBService.getUserByName(userName).subscribe(
      result => {
        this.userSearch = result;
        console.log(this.userSearch);
      },
      error => console.log(error)
    );
  }

  //Find an artist/band by a keyword
  getArtistByName(keyword: string) {
    this.spotifyService.getArtistByName(keyword).subscribe(
      result => {
        this.artistSearch = result;
        console.log(this.artistSearch);
      },
      error => console.log(error)
    );
  }

  //Find a song by a keyword
  getSongByName(songName: string) {
    this.spotifyService.getSongByName(songName).subscribe(
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
    this.songSearch = null;
    this.artistSearch = null;
    this.userSearch = null;

    this.songTitle = form.form.value.search;
    console.log(this.songTitle);

    if (form.form.value.category == 1) {
      this.getSongByName(this.songTitle);
    }

    else if (form.form.value.category == 2) {
      this.getArtistByName(this.songTitle);
    }

    else {
      this.getUserByName(this.songTitle);
    }


  }
}

