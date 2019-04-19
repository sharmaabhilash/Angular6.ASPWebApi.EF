import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs";
import { ApiUrls } from "../apiurls";
import { LoginUser } from "./loginUser";

@Injectable()
export class LoginService {

    constructor(private httpClient: HttpClient, private apiUrls: ApiUrls) { }
    
    PostLoginUserUrl: string = this.apiUrls.ROOT_URL + this.apiUrls.POST_LOGINUSER_URL;
    PostTokenUrl: string = this.apiUrls.ROOT_URL + 'api/token';

    generateToken(loginUser: LoginUser): Observable<any> {
        
        let urlSearchParams = new URLSearchParams();
        urlSearchParams.set('grant_type', 'password');
        urlSearchParams.set('username', loginUser.EmailId);
        urlSearchParams.set('password', loginUser.Password);

        let body = urlSearchParams.toString();
        var httpHeaders = new HttpHeaders({
            'Content-Type': 'application/x-www-form-urlencoded'
        });
        return this.httpClient.post(this.PostTokenUrl, body, { headers: httpHeaders });
    }

    loginUser(loginUser: LoginUser, token: any): Observable<any> {
        
        var httpHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + token
        });
        return this.httpClient.post(this.PostLoginUserUrl, loginUser, { headers: httpHeaders });
    }
}