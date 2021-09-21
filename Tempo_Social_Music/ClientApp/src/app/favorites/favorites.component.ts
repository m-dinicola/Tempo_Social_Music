import { Component, OnInit } from '@angular/core';
import { Favorites } from '../models/Favorites';
import { FavoritesService } from '../services/favorites.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {

  jamsList: Favorites[] = [];

  constructor(private favoritesService: FavoritesService  ) { }

  ngOnInit(): void {
  }

  //Gets the favorites list for a user with a given ID.
  //KS
  getJamsList(userPk: number) {
    this.favoritesService.getJamsList(userPk).subscribe(
      result => {
        this.jamsList = result;
        console.log(this.jamsList)
      },
      error => console.log(error)
    );
  }

  //Adds a favorite to the current user's favorite list.
  //KS
  addJam(jamName: Favorites) {
    this.favoritesService.addJam(jamName).subscribe(
      result => {
        console.log(this.jamsList);
      },
      error => console.log(error)
    );
  }

  //Delete a favorite based on the jam name, and the ID of the current user.
  //KS
  deleteJam(jamName: Favorites, userPK: number) {
    this.favoritesService.deleteJam(jamName).subscribe(
      result => {
        console.log(this.jamsList);
        this.getJamsList(userPK);
      },
      error => console.log(error)
    );
  }
}
