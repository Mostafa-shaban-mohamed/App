import { environment } from "../../../enviroment/enviroment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
//import { Observable } from 'rxjs/Observable';
import { Injectable } from "@angular/core";
import { loginModel } from "../models/login.model";

const api = environment.config.apiUrl;

@Injectable({ providedIn: 'root' })
export class AuthService {

  private _options = {
    headers: new HttpHeaders(
      {
        'Content-Type': 'application/json',
        //'Authorization': 'Bearer ' + localStorage.getItem('token')
      }
    )};

  constructor(private http: HttpClient) {

  }

  login(admin: loginModel) {
    return this.http.post(api + 'Admin/login', admin, this._options);
  }

}
