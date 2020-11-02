import {Component, OnInit} from '@angular/core';

import {ApiService} from '../../shared/services/api.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  constructor( private apiService: ApiService) {
  }

  ngOnInit() {
  }

}
