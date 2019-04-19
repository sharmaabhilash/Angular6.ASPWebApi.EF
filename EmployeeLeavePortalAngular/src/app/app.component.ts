import { Component, HostListener } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [CookieService]
})
export class AppComponent {

  constructor(private cookieService: CookieService) { }

  @HostListener("window:onbeforeunload",["$event"])
  clearCookies(event){
    this.cookieService.delete("CookieData", "/");
  }
}
