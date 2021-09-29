import { TempoUser } from './../models/TempoUser';
import { Component, Input, OnInit } from '@angular/core';
import { Connection } from '../models/Connection';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

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


  @Input() userNum: number;
  unumOffload: number;
  connections: number[];

  constructor(private tempoDBService: TempoDBAPIService) {
  }

  ngOnInit(): void {
    console.log("Start connection onInit. userNum ="+this.userNum);
    this.unumOffload = this.userNum;
    if(this.userNum){
      this.tempoDBService.getFriends(this.userNum).subscribe(
        result=>{
          console.log(`Just got ${result} for userNum=${this.unumOffload} in connection init.`);
          result.forEach(this.parseConnection);
        }
      )
    }
  }

  parseConnection(connection:Connection):number{
    console.log(`Parsing a connection for uNumOffload=${this.unumOffload}.`);
    return (connection.User1==this.unumOffload)?connection.User1:connection.User2;
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
