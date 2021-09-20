import { Component, Input, OnInit } from '@angular/core';
import { Song } from '../models/Song';
import { SpotifyapiService } from '../services/spotifyapi.service';

@Component({
  selector: 'app-song',
  templateUrl: './song.component.html',
  styleUrls: ['./song.component.css']
})
export class SongComponent implements OnInit {

  @Input() song: Song;

  constructor(private spotifyService: SpotifyapiService) { }


  ngOnInit() {

  }

}
