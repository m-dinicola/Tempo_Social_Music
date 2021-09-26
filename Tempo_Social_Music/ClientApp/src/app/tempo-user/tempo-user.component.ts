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

  constructor(private tempoDBService: TempoDBAPIService) { }


  ngOnInit() {

    console.log(this.tempouser.loginName);

  }

}
