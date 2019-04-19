import { Injectable } from "@angular/core";
import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";
import { Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs";
import { ApiUrls } from "../apiurls";
import { ApplyLeave } from "./applyleave";
import { CookieService } from "../../../node_modules/ngx-cookie-service";

@Injectable()
export class ApplyLeaveService {

    cookieResponse: any;
    constructor(private httpClient: HttpClient, private apiUrls: ApiUrls, private cookies: CookieService) { }

    PostApplyLeave: string = this.apiUrls.ROOT_URL + this.apiUrls.POST_APPLYLEAVE_URL;
    GetUserLeave: string = this.apiUrls.ROOT_URL + this.apiUrls.GET_USERLEAVE_URL;
    PutApplyLeave: string = this.apiUrls.ROOT_URL + this.apiUrls.PUT_APPLYLEAVE_URL;

    applyLeaveSave(applyLeave: ApplyLeave): Observable<any> {
        this.cookieResponse = JSON.parse(this.cookies.get('CookieData'));
        
        let id: string  = '';
        let accessToken: string = '';

        id = this.cookieResponse.UserId;
        accessToken = this.cookieResponse.AccessToken;

        var httpHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        });

        const params = new HttpParams().set('id', id);
        return this.httpClient.post(this.PostApplyLeave, applyLeave, { params, headers: httpHeaders });
    }

    getUserLeave(leaveId: any): Observable<ApplyLeave> {
        this.cookieResponse = JSON.parse(this.cookies.get('CookieData'));
        
        let id: string  = '';
        let accessToken: string = '';

        id = this.cookieResponse.UserId;
        accessToken = this.cookieResponse.AccessToken;

        var httpHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        });

        const params = new HttpParams().set('leaveId', leaveId);
        return this.httpClient.request<ApplyLeave>("GET", this.GetUserLeave, { params, headers: httpHeaders });
    }

    updateLeave(applyLeave: ApplyLeave, leaveId: any): Observable<any> {
        this.cookieResponse = JSON.parse(this.cookies.get('CookieData'));
        
        let id: string  = '';
        let accessToken: string = '';

        id = this.cookieResponse.UserId;
        accessToken = this.cookieResponse.AccessToken;

        var httpHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        });

        const params = new HttpParams().set('id', leaveId);
        return this.httpClient.put(this.PutApplyLeave, applyLeave, { params, headers: httpHeaders });
    }
}