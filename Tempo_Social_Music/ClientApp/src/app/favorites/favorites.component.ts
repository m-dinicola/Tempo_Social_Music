import { Component, Input, OnInit } from '@angular/core';
import { Favorites } from '../models/Favorites';
import { FavoritesService } from '../services/favorites.service';
import { SpotifyapiService } from '../services/spotifyapi.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {

  @Input() jams: Favorites[];
  @Input() isFavorite: Favorites;

  constructor(private favoritesService: FavoritesService, private spotifyService: SpotifyapiService) { }

  ngOnInit(): void {

  }

  //Gets the favorites list for a user with a given ID.
  //KS
  getJam(userPk: number) {
    this.favoritesService.getJams(userPk).subscribe(
      result => {
        this.jams = result;
        console.log(this.jams)
      },
      error => console.log(error)
    );
  }

  deleteJam(favorite: Favorites) {
    this.favoritesService.deleteJam(favorite).subscribe(
      result => {
        console.log(this.jams);
      },
      error => console.log(error)
    );
  }

}
