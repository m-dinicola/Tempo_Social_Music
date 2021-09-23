import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TempoUser } from '../models/TempoUser';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {

  apiUri: string = "https://mixr.azurewebsites.net/api/tempodb";
  newDataAdded = new EventEmitter<string>();

  input: string;
  newUser: TempoUser = { userPk: 0, loginName: '', firstName: '', lastName: '', streetAddress: '', state: '', zipCode: '', userBio: '' };
  userList: TempoUser[] = [];
  constructor(private http: HttpClient, private tempoDBService: TempoDBAPIService) {

    console.log(this.apiUri)
  }

  ngOnInit(): void {
  }

  //Create a new user object
  //KS
  createUser(newUser: TempoUser) {
    this.tempoDBService.createUser(newUser).subscribe(
        result => {
          this.newUser = result;
          console.log(this.newUser);
        },
        error => console.log(error)
      );
    }

  //This will execute creating a new user when the button it is linked to is clicked.
  //KS
  onSubmit(form: NgForm) {
    this.newUser = form.form.value;
    console.log(this.newUser);
    this.createUser(this.newUser);
  }
}
