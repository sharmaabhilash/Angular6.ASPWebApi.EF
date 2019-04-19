import { Component, OnInit } from '@angular/core';
import { CookieService } from '../../../node_modules/ngx-cookie-service';
import { Router } from '../../../node_modules/@angular/router';
import { GrantLeave } from './grantleave';
import { GrantLeaveService } from './grantleave.service';
import { ApiUrls } from '../apiurls';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-grantleave',
  templateUrl: './grantleave.component.html',
  styleUrls: ['./grantleave.component.css'],
  providers: [GrantLeaveService, ApiUrls, CookieService]
})
export class GrantleaveComponent implements OnInit {

  allLeavesList: GrantLeave[];
  recordPerPage: number = 10;
  pageNo: number = 1;
  totalRecordList: number = 0;
  disablePrevBtn: boolean;
  disableNextBtn: boolean;
  totalLeaves: number = 0;

  constructor(private cookie: CookieService, private router: Router, private grantLeaveService: GrantLeaveService ) { }

  ngOnInit() {
    let cookieExists: boolean = this.cookie.check('CookieData');
    if(cookieExists == false) {
      this.router.navigate(['/login']);
    }
    this.getAllLeaveData();
  }

  getAllLeaveData(): void {
    this.grantLeaveService.getAllLeaveData(this.pageNo).subscribe(res => this.apiResponse(res));
  }

  apiResponse(res): void {
    this.allLeavesList = res.AllLeaveData;
    this.totalRecordList = res.TotalLeaveList;
    this.disablePrevBtn = res.DisablePrevBtn;
    this.disableNextBtn = res.DisableNextBtn;
    this.pageNo = res.PageNo;
    this.recordPerPage = res.RecordPerPage;
    this.totalLeaves = res.TotalLeaves;
  }

  getNextLeaveData(): void {
    let pageno = this.pageNo + 1;
    this.grantLeaveService.getAllLeaveData(pageno).subscribe(res => this.apiResponse(res));
  }

  getPrevLeaveData(): void {
    let pageno = this.pageNo - 1;
    this.grantLeaveService.getAllLeaveData(pageno).subscribe(res => this.apiResponse(res));
  }

  approveLeave(leaveid: any): void {
    this.grantLeaveService.approveLeave(leaveid).subscribe(res => this.grantResponse(res));
  }

  unapproveLeave(leaveid: any): void {
    this.grantLeaveService.unapproveLeave(leaveid).subscribe(res => this.grantResponse(res));
  }

  grantResponse(res): void {
    if(res == "success") {
      Swal.fire(
        'Success',
        'Leave Approved/Unapproved Successfully.',
        'success'
      )
    }
    else {
      Swal.fire(
        'Failed',
        res,
        'error'
      )
    }
    this.getAllLeaveData();
  }
}
