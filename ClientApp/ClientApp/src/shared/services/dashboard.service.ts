import { environment } from "../../../enviroment/enviroment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
//import { Observable } from 'rxjs/Observable';
import { Injectable } from "@angular/core";

const api = environment.config.apiUrl;

@Injectable({ providedIn: 'root' })
export class DashboardService {

  private _options = {
    headers: new HttpHeaders(
      {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      }
    )
  };

  constructor(private http: HttpClient) {

  }

  pieChart() {
    return this.http.get(api + 'User/PieChart', this._options);
  }

  lineChart() {
    return this.http.get(api + 'User/LineChart', this._options);
  }
}
