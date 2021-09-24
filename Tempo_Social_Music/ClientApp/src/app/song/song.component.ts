import { Component, Input, OnInit } from '@angular/core';
import { Favorites } from '../models/Favorites';
import { Song } from '../models/Song';
import { FavoritesService } from '../services/favorites.service';
import { SpotifyapiService } from '../services/spotifyapi.service';

@Component({
  selector: 'app-song',
  templateUrl: './song.component.html',
  styleUrls: ['./song.component.css']
})
export class SongComponent implements OnInit {

  @Input() song: Song;
  jam: Favorites = {Favorite:0, UserId:0, SpotTrack:"", SpotArtist:""};

  constructor(private spotifyService: SpotifyapiService, private favoritesService: FavoritesService) { }


  ngOnInit() {

  }

  //Adds a favorite to the current user's favorite list.
  //KS
  addJam(jamId: string) {
    this.jam.SpotTrack = jamId;
    this.favoritesService.addJam(this.jam).subscribe(
      result => {
        console.log(this.jam);
      },
      error => console.log(error)
    );
  }

}
