import { Component, Input, OnInit } from '@angular/core';
import { TempoUser } from '../models/TempoUser';
import { TempoDBAPIService } from '../services/tempo-db-api.service';


@Component({
  selector: 'app-bio',
  templateUrl: './bio.component.html',
  styleUrls: ['./bio.component.css']
})
export class BioComponent implements OnInit {

  userBio: string;
  @Input() tempoUser: TempoUser;

  constructor(private tempoDBService: TempoDBAPIService) { }

  ngOnInit(): void {
  }

//  updateBio(newBio: string) {
//    this.tempoDBService.updateBio(newBio).subscribe(
//      result => {
//        this.userBio = result;
//        console.log(this.userBio);

//      }
//    )
//}


}
