import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ApiUrls } from '../apiurls';
import { ViewLeaveService } from './viewleaves.service';
import { ViewLeaves } from './viewleaves';
import { Router } from '../../../node_modules/@angular/router';
import { CookieData } from '../cookiedata';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-viewleaves',
  templateUrl: './viewleaves.component.html',
  styleUrls: ['./viewleaves.component.css'],
  providers: [ViewLeaveService, ApiUrls, CookieService]
})

export class ViewleavesComponent implements OnInit {

  allLeavesList: ViewLeaves[];
  recordPerPage: number = 10;
  pageNo: number = 1;
  totalRecordList: number = 0;
  disablePrevBtn: boolean;
  disableNextBtn: boolean;
  totalLeaves: number = 0;

  constructor(private viewLeaveService: ViewLeaveService, private cookie: CookieService, private router: Router) { }

  ngOnInit() {
    let cookieExists: boolean = this.cookie.check('CookieData');
    if(cookieExists == false) {
      this.router.navigate(['/login']);
    }
    this.getLeaveData();
  }

  getLeaveData(): void {
    this.viewLeaveService.getLeaveData(this.pageNo).subscribe(res => this.apiResponse(res));
  }

  apiResponse(res): void {
    this.allLeavesList = res.AllLeaveData;
    console.log(this.allLeavesList);
    this.totalRecordList = res.TotalLeaveList;
    this.disablePrevBtn = res.DisablePrevBtn;
    this.disableNextBtn = res.DisableNextBtn;
    this.pageNo = res.PageNo;
    this.recordPerPage = res.RecordPerPage;
    this.totalLeaves = res.TotalLeaves;
  }

  getNextLeaveData(): void {
    let pageno = this.pageNo + 1;
    this.viewLeaveService.getLeaveData(pageno).subscribe(res => this.apiResponse(res));
  }

  getPrevLeaveData(): void {
    let pageno = this.pageNo - 1;
    this.viewLeaveService.getLeaveData(pageno).subscribe(res => this.apiResponse(res));
  }

  editLeaveApplication(LeaveUniqueId: any): void {
    this.router.navigate(['/applyleave'], { queryParams: { id: LeaveUniqueId } });
  }

  deleteLeaveApplication(LeaveUniqueId: any): void {
    this.viewLeaveService.deleteLeaveApplication(LeaveUniqueId).subscribe(res => this.deleteResponse(res));
  }

  deleteResponse(res): void {
    if (res == "success")
    {
      Swal.fire(
        'Success',
        'Leave Application Deleted Successfully.',
        'success'
      );
    }
    else 
    {
      Swal.fire(
        'Failed',
        res,
        'error'
      );
    }
    this.getLeaveData();
  }
}
