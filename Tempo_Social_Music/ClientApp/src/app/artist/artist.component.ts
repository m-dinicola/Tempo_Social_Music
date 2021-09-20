import { Component, Input, OnInit } from '@angular/core';
import { Artists } from '../models/Artists';
import { SpotifyapiService } from '../services/spotifyapi.service';

@Component({
  selector: 'app-artist',
  templateUrl: './artist.component.html',
  styleUrls: ['./artist.component.css']
})
export class ArtistComponent implements OnInit {

  @Input() artist: Artists;

  constructor(private spotifyService: SpotifyapiService) { }


  ngOnInit() {

  }

}
