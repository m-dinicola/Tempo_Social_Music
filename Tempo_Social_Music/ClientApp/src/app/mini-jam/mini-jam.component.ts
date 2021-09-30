import { SpotifyapiService } from './../services/spotifyapi.service';
import { FavoritesService } from './../services/favorites.service';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Favorites } from '../models/Favorites';
import { Song } from '../models/song';
import { Artists } from '../models/Artists';

@Component({
  selector: 'app-mini-jam',
  templateUrl: './mini-jam.component.html',
  styleUrls: ['./mini-jam.component.css']
})
export class MiniJamComponent implements OnInit {

  @Input() userPk: number;
  @Input() song: Song;
  @Input() artist: Artists;
  @Input() isFavorite: string;
  jam: Favorites = { favorite: 0, userId: 0, spotTrack: null, spotArtist: null };
  @Output() faveSet = new EventEmitter();
  constructor(private favoritesService: FavoritesService, private spotifyService:SpotifyapiService) { }

  ngOnInit(): void {
    if(this.userPk && this.userPk != 0){
      this.favoritesService.getJams(this.userPk).subscribe(
        result=>{
          console.log(`Setting jams for user ${this.userPk}.`)
          if (result.length > 0)
          {
            console.log(`onInit userPk!=0, faves.length > 0, getting random track from faves.`)
            var rand:number = this.getRandomUnder(result.length);
            console.log(`Chose favorite ${result[rand].favorite}.`)
            if(result[rand].spotTrack){
              console.log(`Fave ${rand} was a song.`);
              this.getSong(result[rand].spotTrack);
            }
            else{
              console.log(`Fave ${rand} was an artist.`);
              this.getArtist(result[rand].spotArtist);
            }
          }
        }
      )
    }
  }

  //MD
  songIsSet():void{
    this.faveSet.emit(this.song.name);
  }

  artistIsSet():void{
    this.faveSet.emit(this.artist.name)
  }
  //Adds a favorite to the current user's favorite list.
  //KS
  addJam(jamId: string) {
    this.jam.spotTrack = jamId;
    this.favoritesService.addJam(this.jam).subscribe(
      result => {
        console.log(this.jam);
      },
      error => console.log(error)
    );
  }

  getRandomUnder(max: number):number{
    return Math.floor(Math.random()*max);
  }

  isSongFave(favorite:Favorites):boolean{
    return !!favorite.spotTrack;
  }

  getArtist(artistId:string):void{
    this.spotifyService.getArtistById(artistId).subscribe(
      result =>{
        this.artist = result;
        this.artistIsSet();
        console.log(`MiniJam getSong(${artistId}) =>`  + this.artist);
      }
    )
  }

  getSong(trackId:string):void{
    this.spotifyService.getSongById(trackId).subscribe(
      result =>{
        this.song = result;
        this.songIsSet();
        console.log(`MiniJam getSong(${trackId}) =>`  + this.song);
      }
    )
  }
}


