import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs";
import { Position } from './position';
import { ApiUrls } from "../apiurls";
import { Department } from "./department";
import { RegisterUser } from "./registerUser";
import { CompileShallowModuleMetadata } from "../../../node_modules/@angular/compiler";

@Injectable()
export class RegisterService {

    response: Observable<any>;

    constructor(private httpClient: HttpClient, private apiUrls: ApiUrls) { }

    GetPositionUrl: string = this.apiUrls.ROOT_URL + this.apiUrls.GET_POSITION_URL;
    GetDepartmentUrl: string = this.apiUrls.ROOT_URL + this.apiUrls.GET_DEPARTMENT_URL;
    PostRegisterUserUrl: string = this.apiUrls.ROOT_URL + this.apiUrls.POST_REGISTERUSER_URL;
    PostUploadFile: string = this.apiUrls.ROOT_URL + this.apiUrls.POST_UPLOAD_FILE;

    getPositionData(): Observable<Position[]> {
        return this.httpClient.request<Position[]>("GET", this.GetPositionUrl);
    }

    getDepartmentData(): Observable<Department[]> {
        return this.httpClient.request<Department[]>("GET", this.GetDepartmentUrl);
    }

    saveRegisterUser(registerUser: RegisterUser): Observable<any> {
        var headerOptions = new Headers({'Content-Type':'application/json'});
        var requestOptions = new RequestOptions({ headers: headerOptions });

        return this.httpClient.post(this.PostRegisterUserUrl, registerUser);
    }

    uploadFile(formData: any): Observable<any> {
        var headerOptions = new Headers({'Content-Type':'application/json'});
        var requestOptions = new RequestOptions({ headers: headerOptions });
        
        return this.httpClient.post(this.PostUploadFile, formData);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body;
    }

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }
}