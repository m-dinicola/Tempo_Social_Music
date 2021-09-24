import { Component, Input, OnInit } from '@angular/core';
import { Connection } from '../models/Connection';
import { TempoDBAPIService } from '../services/tempo-db-api.service';

@Component({
  selector: 'app-connection',
  templateUrl: './connection.component.html',
  styleUrls: ['./connection.component.css']
})
export class ConnectionComponent implements OnInit {

  userConnection: number;
  @Input() connection: Connection;

  constructor(private tempoDBService: TempoDBAPIService) { }

  ngOnInit(): void {
  }

}
