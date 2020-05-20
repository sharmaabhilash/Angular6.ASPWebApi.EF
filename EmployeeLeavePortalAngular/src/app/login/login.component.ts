import { Component, OnInit } from '@angular/core';
import { ApiUrls } from '../apiurls';
import { LoginUser } from './loginUser';
import { HttpResponse } from '@angular/common/http';
import { LoginService } from './login.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { CookieData } from '../cookiedata';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [LoginService, ApiUrls, CookieService]
})
export class LoginComponent implements OnInit {
  
  login = new LoginUser;
  cookieData = new CookieData;
  token: string;
  
  constructor(private loginService: LoginService, private router: Router, private cookies: CookieService) { }

  ngOnInit() {
  }

  loginUserOperation(): void {
    this.loginService.generateToken(this.login).subscribe(res => this.loginUser(res.access_token));
  }

  loginUser(token: any): void {
    this.token = token;
    this.loginService.loginUser(this.login, token).subscribe(res => this.apiResponse(res));
  }

  apiResponse(res) {
    if (res.status == "success")
    {
      this.cookieData.AccessToken = this.token;
      this.cookieData.UserId = res.id;
      this.cookieData.ApprovalPermission = res.approvalPermission;
      this.cookieData.UserName = res.userName;
      this.cookieData.ImageSrc = "http://192.168.1.10:75/UploadFile/" + res.imageName;
      this.cookies.set('CookieData', JSON.stringify(this.cookieData), 0.041666);
      //this.router.navigate(['/applyleave'], { queryParams: { id: res.id } });
      this.router.navigate(['/applyleave']);
    }
    else {
      Swal.fire(
        'Failed',
        'Oops! Something went wrong. Please check login credentials.',
        'error'
      )
    }
  }

  validateLogin(): boolean {
    let flag = true;

    document.getElementById('divEmailId').classList.remove('error');
    document.getElementById('divPassword').classList.remove('error');
    
    let pattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/i;

    if(this.login.EmailId == "" || !pattern.test(this.login.EmailId))
    {
      document.getElementById('divEmailId').classList.add('error');
      flag = false;
    }
    if(this.login.Password == "")
    {
      document.getElementById('divPassword').classList.add('error');
      flag = false;
    }

    return flag;
  }
}
