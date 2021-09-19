import { Component, Input, OnInit } from '@angular/core';
import { TempoUser } from '../models/TempoUser';
import { Artists } from '../models/Artists';
import { SpotifyapiService } from '../services/spotifyapi.service';
import { TempoDBAPIService } from '../services/tempo-db-api.service';
import { Song } from '../models/Song';
import { NgForm } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(private router:Router, private tempoDBService: TempoDBAPIService, private spotifyService: SpotifyapiService, private route: ActivatedRoute) { }

  songTitle: string;
  userSearch: TempoUser[] = [];
  artistSearch: Artists;
  songSearch: Song;

  ngOnInit() {
    /*console.log(this.songTitle*/);
  }

  //Find a user by user name
  //KS
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
  //KS
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
  //KS
  getSongByName(keyword: string): Song {
    this.spotifyService.getSongByName(keyword).subscribe(
      result => {
        this.songSearch = result;
        console.log(this.songSearch);
        
      },
      error => console.log(error)
    );
    return this.songSearch;
  }

  //This will utilize the search form. It will take in a string "keyword" and seach
  //for that keyword when the search button is clicked.
  //KS
  onSubmit(form: NgForm) {
    this.songTitle = form.form.value;
    console.log(this.songTitle);
    this.getUserByName(this.songTitle);
    this.router.navigate(['/create-user'])

  }
}


