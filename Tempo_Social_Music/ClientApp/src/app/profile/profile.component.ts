import { Component, OnInit } from '@angular/core';
import { FavoritesService } from '../services/favorites.service';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private tempoDBService: TempoDBAPIService, private favoritesService: FavoritesService) { }

  ngOnInit(): void {
  }

}
