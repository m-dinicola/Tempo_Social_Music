import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TempoUser } from '../models/TempoUser';
import { TempoDBAPIService } from '../services/tempo-db-api.service';


@Component({
  selector: 'app-bio',
  templateUrl: './bio.component.html',
  styleUrls: ['./bio.component.css']
})
export class BioComponent implements OnInit {

  
  userBio: string = "this property is a test";
  @Input() tempoUser: TempoUser = {
    userPk: 0,
    loginName: "",
    firstName: "",
    lastName: "",
    streetAddress: "",
    state: "",
    zipCode: "",
    userBio: ""
  };

  constructor(private tempoDBService: TempoDBAPIService, private http: HttpClient) { }

  ngOnInit(): void {
  }

  //Update an existing user bio.
  //KS
  updateBio(newBio: string) {
    this.tempoDBService.updateBio(newBio).subscribe(
      result => {
        newBio = result.userBio;
        console.log(this.userBio);
      },
      error => {
        console.log(error)
      }
    );
  }

  //getUserBio(bio:TempoUser) {
  //  this.tempoDBService.getUserBio(bio.userBio).subscribe(
  //    result => {
  //      this.userBio = result
  //    },
  //    error => console.log(error)
  //  );
  //}

  updateProfile(newInfo: TempoUser) {
    this.tempoDBService.updateProfile(newInfo).subscribe(
      result => {
        newInfo = result;
        console.log(newInfo)
      },
      error => {
        console.log(error);
      }
    )
  }

  onSubmit(form: NgForm) {
    this.tempoUser = form.form.value;
    console.log(this.tempoUser);
    this.updateProfile(this.tempoUser);
  }

  reloadPage() {
    window.location.reload();
  }
}
