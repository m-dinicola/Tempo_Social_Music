import { Component, OnInit } from '@angular/core';
import { TempoUser } from '../models/TempoUser';
import { SpotifyapiService } from '../services/spotifyapi.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(private spotifyService: SpotifyapiService) { }

  userSearch: TempoUser[] = [];

  ngOnInit(): void {
  }

}
