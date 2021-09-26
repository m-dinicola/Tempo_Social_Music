import { Component, Input, OnInit } from '@angular/core';
import { Connection } from '../models/Connection';
import { TempoDBAPIService } from '../services/tempo-db-api.service';
import { TempoUser } from '../models/TempoUser';

@Component({
  selector: 'app-connection',
  templateUrl: './connection.component.html',
  styleUrls: ['./connection.component.css']
})
export class ConnectionComponent implements OnInit {

  userConnect: TempoUser = {
    userPk: 0,
    loginName: "",
    firstName: "",
    lastName: "",
    streetAddress: "",
    state: "",
    zipCode: "",
    userBio: ""
  };


  @Input() connection: Connection;

  constructor(private tempoDBService: TempoDBAPIService) { }

  ngOnInit(): void {
  }


  //addUserFriend(userConnect: string) {
  //  this.tempoDBService.addUserFriend(userConnect).subscribe(
  //    result => {
  //      this.addUserFriend = result;
  //      console.log(this.addUserFriend);
  //    },
  //    error => {
  //      console.log(error)
  //    }
  //  );
  //}
}
