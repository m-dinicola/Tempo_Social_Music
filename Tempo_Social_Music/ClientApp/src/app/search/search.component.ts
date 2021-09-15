import { Component, OnInit } from '@angular/core';
import { TempoUser } from '../models/TempoUser';
import { SpotifyapiService } from '../services/spotifyapi.service';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(private tempoDBService: TempoDBAPIService) { }

  userSearch: TempoUser[] = [];

  ngOnInit(): void {
  }

}

getUserList(){
  this.tempoDBService.getUserList().subscribe(
    result => {
      this.userSearch = result;
      console.log(this.userSearch);
    },
    error => console.log(error)
  );
}
