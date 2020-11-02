import { Component, OnInit } from '@angular/core';
import {ApiService} from '../../shared/services/api.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  ngOnInit() {
  }

}
