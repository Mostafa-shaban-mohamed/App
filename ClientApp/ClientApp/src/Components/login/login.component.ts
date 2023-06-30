import { Serializer } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { loginModel } from '../../shared/models/login.model';
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public login: loginModel = {
    email: '',
    password: ''
  };

  errorMessage: string = "";
  isError: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {}

  onChange(event: any) {
    //console.log(event.target.value);
    if(event.target.name == 'email')
      this.login.email = event.target.value;
    else if (event.target.name == 'password')
      this.login.password = event.target.value;
  }

  onSubmit() {
    this.authService.login(this.login).subscribe((resp) => {
      var data = JSON.stringify(resp);
      //console.log(JSON.parse(data).data);
      if (JSON.parse(data).isSuccess == false) {
        this.errorMessage = JSON.parse(data).errorMessage;
        this.isError = true;
      } else {
        this.isError = false;
        localStorage.setItem("token", JSON.parse(data).data);
        this.router.navigateByUrl('/dashboard');
      }
    });
  }
}
