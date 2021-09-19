import { Component, OnInit } from '@angular/core';
import { TempoUser } from '../models/TempoUser';
import { SpotifyapiService } from '../services/spotifyapi.service';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  userList: TempoUser[];

  constructor(private tempoDBService: TempoDBAPIService, private spotifyService: SpotifyapiService ) { }

  ngOnInit(): void {
  }

}
