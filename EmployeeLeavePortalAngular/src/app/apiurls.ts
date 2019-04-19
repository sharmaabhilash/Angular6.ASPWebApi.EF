export class ApiUrls {
    //ROOT_URL: string = 'http://localhost:51227/';
    ROOT_URL: string = 'http://192.168.1.10:75/';
    GET_POSITION_URL: string = 'api/home/position';
    GET_DEPARTMENT_URL: string = 'api/home/department';
    POST_REGISTERUSER_URL: string = 'api/home/saveregisteruser';
    POST_LOGINUSER_URL: string = 'api/home/loginuser';
    POST_APPLYLEAVE_URL: string = 'api/leave/applyleave';
    GET_ALLAPPLIEDLEAVE_USERWISE_URL: string = 'api/leave/getuserleaves';
    GET_ALLAPPLIEDLEAVE_DEPTWISE_URL: string = 'api/leave/getallleaves';
    PUT_APPROVELEAVE_URL: string = 'api/leave/approveleave';
    PUT_UNAPPROVELEAVE_URL: string = 'api/leave/unapproveleave';
    GET_USERLEAVE_URL: string = 'api/leave/getuserleave';
    PUT_APPLYLEAVE_URL: string = 'api/leave/updateappliedleave';
    DELETE_APPLYLEAVE_URL: string = 'api/leave/deleteappliedleave';
    POST_UPLOAD_FILE: string = 'api/home/uploadfile';
}