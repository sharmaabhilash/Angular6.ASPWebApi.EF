import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from '../../../node_modules/ngx-cookie-service';

@Component({
  selector: 'app-menues',
  templateUrl: './menues.component.html',
  styleUrls: ['./menues.component.css']
})
export class MenuesComponent implements OnInit {

  permission: boolean;
  userName: string;
  imageSrc: string;

  constructor(private activateRoute: ActivatedRoute, private cookies: CookieService, private router: Router) {
    // this.activateRoute.queryParams.subscribe(params => {
    //   this.id = params['id'];
    // });
  }

  ngOnInit() {
    let cookie = JSON.parse(this.cookies.get('CookieData'));
    if(cookie.ApprovalPermission == 'Yes')
    {
      this.permission = true;
    }
    else
    {
      this.permission = false;
    }
    this.userName = cookie.UserName;
    this.imageSrc = cookie.ImageSrc;
  }

  logoutApplication(): void {
    this.cookies.delete('CookieData', '/');
    this.router.navigate(['/login']);
  }
}
