import { Component, Input, OnInit } from '@angular/core';
import { Song } from '../models/Song';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

@Component({
  selector: 'app-song',
  templateUrl: './song.component.html',
  styleUrls: ['./song.component.css']
})
export class SongComponent implements OnInit {

  /*@Input() keyword: string;*/

  constructor(private tempoDBService: TempoDBAPIService) { }

  ngOnInit(): void {
    /*console.log(this.keyword);*/
  }

}
