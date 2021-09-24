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

  jam: Favorites;

  constructor(private favoritesService: FavoritesService, private spotifyService: SpotifyapiService  ) { }

  ngOnInit(): void {
  }

  //Gets the favorites list for a user with a given ID.
  //KS
  getJam(userPk: number) {
    this.favoritesService.getJam(userPk).subscribe(
      result => {
        this.jam = result;
        console.log(this.jam)
      },
      error => console.log(error)
    );
  }

  //Adds a favorite to the current user's favorite list.
  //KS
  addJam(jamName: Favorites) {
    this.favoritesService.addJam(jamName).subscribe(
      result => {
        console.log(this.jam);
      },
      error => console.log(error)
    );
  }

  //Delete a favorite based on the jam name, and the ID of the current user.
  //KS
  deleteJam(jamName: Favorites, userPK: number) {
    this.favoritesService.deleteJam(jamName).subscribe(
      result => {
        console.log(this.jam);
        this.getJam(userPK);
      },
      error => console.log(error)
    );
  }
}
