import { Component, Input, OnInit } from '@angular/core';
import { Favorites } from '../models/Favorites';
import { Song } from '../models/Song';
import { FavoritesService } from '../services/favorites.service';
import { SpotifyapiService } from '../services/spotifyapi.service';

@Component({
  selector: 'app-song',
  templateUrl: './song.component.html',
  styleUrls: ['./song.component.css']
})
export class SongComponent implements OnInit {

  @Input() song: Song;
  @Input() isFavorite: string;
  jam: Favorites = { favorite: 0, userId: 0, spotTrack: null, spotArtist: null };

  constructor(private spotifyService: SpotifyapiService, private favoritesService: FavoritesService) {
  }


  ngOnInit() {
    console.log("SONG COMPONENT TEST" + this.isFavorite);
    if (this.isFavorite) {
      this.spotifyService.getSongById(this.isFavorite).subscribe(
        result => this.song = result,
        error => console.log(error)
      )
    }
  }

  //Adds a favorite to the current user's favorite list.
  //KS
  addJam(jamId: string) {
    this.jam.spotTrack = jamId;
    this.favoritesService.addJam(this.jam).subscribe(
      result => {
        console.log(this.jam);
      },
      error => console.log(error)
    );
  }

  //Delete a favorite.
  //KS
  deleteJam() {
    //this.favoritesService.deleteJam(this.isFavorite).subscribe(
    //  result => {
    //    console.log(this.jam);
    //  },
    //  error => console.log(error)
    //);
  }

}
