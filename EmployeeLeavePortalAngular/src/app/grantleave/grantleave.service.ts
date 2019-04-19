import { Injectable } from "@angular/core";
import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";
import { Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs";
import { ApiUrls } from "../apiurls";
import { GrantLeave } from "./GrantLeave";
import { CookieService } from "../../../node_modules/ngx-cookie-service";

@Injectable()
export class GrantLeaveService {

    cookieResponse: any;
    constructor(private httpClient: HttpClient, private apiUrls: ApiUrls, private cookies: CookieService) { }

    GetAllLeaves: string = this.apiUrls.ROOT_URL + this.apiUrls.GET_ALLAPPLIEDLEAVE_DEPTWISE_URL;
    PutApproveLeave: string = this.apiUrls.ROOT_URL + this.apiUrls.PUT_APPROVELEAVE_URL;
    PutUnapproveLeave: string = this.apiUrls.ROOT_URL + this.apiUrls.PUT_UNAPPROVELEAVE_URL;

    getAllLeaveData(pageNo: any): Observable<GrantLeave[]> {
        this.cookieResponse = JSON.parse(this.cookies.get('CookieData'));
        
        let id: string  = '';
        let accessToken: string = '';

        id = this.cookieResponse.UserId;
        accessToken = this.cookieResponse.AccessToken;

        var httpHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        });

        const params = new HttpParams().set('id', id).set('pageNo', pageNo);
        return this.httpClient.request<GrantLeave[]>("GET", this.GetAllLeaves, { params, headers: httpHeaders });
    }

    approveLeave(leaveid: any): Observable<any> {
        this.cookieResponse = JSON.parse(this.cookies.get('CookieData'));
        
        let id: string  = '';
        let accessToken: string = '';

        id = this.cookieResponse.UserId;
        accessToken = this.cookieResponse.AccessToken;

        var httpHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        });

        const params = new HttpParams().set('userId', id).set('leaveId', leaveid);
        return this.httpClient.put(this.PutApproveLeave, '', { headers: httpHeaders, params });
    }

    unapproveLeave(leaveid: any): Observable<any> {
        this.cookieResponse = JSON.parse(this.cookies.get('CookieData'));
        
        let id: string  = '';
        let accessToken: string = '';

        id = this.cookieResponse.UserId;
        accessToken = this.cookieResponse.AccessToken;

        var httpHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        });

        const params = new HttpParams().set('userId', id).set('leaveId', leaveid);
        return this.httpClient.put(this.PutUnapproveLeave, '', { headers: httpHeaders, params });
    }
}