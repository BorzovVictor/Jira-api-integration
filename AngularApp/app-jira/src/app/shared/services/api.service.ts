import {Injectable} from '@angular/core';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import CustomStore from 'devextreme/data/custom_store';
import {createStore} from 'devextreme-aspnet-data-nojquery';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  baseUrl = 'https://localhost:44379/api';

  public userDataSource: CustomStore;
  public projectDataSource: CustomStore;

  constructor() {
    this.makeDataSources();
  }

  makeDataSources() {
    this.userDataSource = createStore({
      key: 'key',
      loadUrl: this.baseUrl + '/users',
      // insertUrl: this.url + '/InsertOrder',
      // updateUrl: this.url + '/UpdateOrder',
      // deleteUrl: this.url + '/DeleteOrder',
      onBeforeSend(method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
      }
    });

    this.projectDataSource = createStore({
      key: 'id',
      loadUrl: this.baseUrl + '/projects'
    });
  }

}
