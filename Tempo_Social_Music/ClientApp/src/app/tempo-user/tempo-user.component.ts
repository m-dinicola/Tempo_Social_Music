import { Component, Input, OnInit } from '@angular/core';
import { TempoUser } from '../models/TempoUser';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

@Component({
  selector: 'app-tempo-user',
  templateUrl: './tempo-user.component.html',
  styleUrls: ['./tempo-user.component.css']
})
export class TempoUserComponent implements OnInit {

  @Input() tempouser: TempoUser;
  faveName: string;

  constructor(private tempoDBService: TempoDBAPIService) { }


  ngOnInit() {

    console.log(this.tempouser.loginName);

  }

  displayFave(songName:string):void{
    this.faveName = songName;
  }

}
