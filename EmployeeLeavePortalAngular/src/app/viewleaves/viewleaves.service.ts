import { ApiUrls } from "../apiurls";
import { CookieService } from "ngx-cookie-service";
import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";
import { Observable } from "../../../node_modules/rxjs";
import { ViewLeaves } from "./viewleaves";
import { Injectable } from "@angular/core";

@Injectable()
export class ViewLeaveService {

    cookieResponse: any;
    constructor(private httpClient: HttpClient, private apiUrls: ApiUrls, private cookies: CookieService) { }

    GetAppliedLeavesByUserUrl: string = this.apiUrls.ROOT_URL + this.apiUrls.GET_ALLAPPLIEDLEAVE_USERWISE_URL;
    DeleteApplyLeaveUrl: string = this.apiUrls.ROOT_URL + this.apiUrls.DELETE_APPLYLEAVE_URL;

    getLeaveData(pageNo: any): Observable<ViewLeaves[]> {
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
        return this.httpClient.request<ViewLeaves[]>("GET", this.GetAppliedLeavesByUserUrl, { params, headers: httpHeaders });
    }

    deleteLeaveApplication(leaveId: any): Observable<any> {
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
        return this.httpClient.delete(this.DeleteApplyLeaveUrl, { params, headers: httpHeaders });
    }
}