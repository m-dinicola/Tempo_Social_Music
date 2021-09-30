import { FavoritesService } from './../services/favorites.service';
import { Component, Input, OnInit } from '@angular/core';
import { Artists } from '../models/Artists';
import { SpotifyapiService } from '../services/spotifyapi.service';
import { Favorites } from '../models/Favorites';

@Component({
  selector: 'app-artist',
  templateUrl: './artist.component.html',
  styleUrls: ['./artist.component.css']
})
export class ArtistComponent implements OnInit {

  @Input() artist: Artists;
  @Input() isFavorite: string;
  jam: Favorites = { favorite: 0, userId: 0, spotTrack: null, spotArtist: null }

  constructor(private spotifyService: SpotifyapiService, private favoritesService: FavoritesService) { }


  ngOnInit() {
    console.log("Artist component init "+this.isFavorite);
    if (this.isFavorite) {
      this.spotifyService.getArtistById(this.isFavorite).subscribe(
        result => this.artist = result,
        error => console.log(error)
      )
    }

  }

  //Adds a favorite to the current user's favorite list.
  //KS
  addJam(jamId: string) {
    this.jam.spotArtist = jamId;
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

