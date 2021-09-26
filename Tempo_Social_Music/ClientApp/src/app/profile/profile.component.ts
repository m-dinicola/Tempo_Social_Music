import { TempoUser } from './../models/TempoUser';
import { Component, OnInit } from '@angular/core';
import { Favorites } from '../models/Favorites';
import { FavoritesService } from '../services/favorites.service';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  activeUser: TempoUser = {userPk:0,loginName:null,firstName:null,lastName:null,streetAddress:null,state:null,zipCode:null,userBio:null};
  activeUsersFaves: Favorites[];
  constructor(private tempoDBService: TempoDBAPIService, private favoritesService: FavoritesService) {
  }

  ngOnInit(): void {
    this.tempoDBService.getActiveUser().subscribe(
      result => {
        this.activeUser = result;
        this.getJamsList();
      },
      error => console.log(error)
    );
  }

  getJamsList(): void{
    this.favoritesService.getJams(this.activeUser.userPk).subscribe(
      result => this.activeUsersFaves = result,
      error => console.log(error)
    )
  }
}
