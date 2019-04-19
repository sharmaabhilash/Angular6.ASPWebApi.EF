import { Component, OnInit } from '@angular/core';
import { ApplyLeave } from './applyleave';
import { ApplyLeaveService } from './applyleave.service';
import { ApiUrls } from '../apiurls';
import Swal from 'sweetalert2';
import { CookieService } from '../../../node_modules/ngx-cookie-service';
import { Router, ActivatedRoute } from '../../../node_modules/@angular/router';

@Component({
  selector: 'app-applyleave',
  templateUrl: './applyleave.component.html',
  styleUrls: ['./applyleave.component.css'],
  providers: [ApplyLeaveService, ApiUrls, CookieService]
})
export class ApplyleaveComponent implements OnInit {

  applyLeave = new ApplyLeave;
  id: any;

  constructor(private applyLeaveService: ApplyLeaveService, private cookie: CookieService, private router: Router, private activatedRoute: ActivatedRoute) { 
    // , private route: ActivatedRoute
    // this.route.queryParams.subscribe(params => {
    //   this.id = params['id'];
    // });
  }

  ngOnInit() {
    let cookieExists: boolean = this.cookie.check('CookieData');
    if(cookieExists == false) {
      this.router.navigate(['/login']);
    }

    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
    });
    if (this.id != undefined) {
      this.getLeaveForEdit(this.id);
    }
  }

  getLeaveForEdit(leaveId: any): void {
    this.applyLeaveService.getUserLeave(leaveId).subscribe(res => this.bindLeaveData(res));
  }

  bindLeaveData(res: any) {
    if(res.FromDate != undefined || res.FromDate != null)
    {
      this.applyLeave.FromDate = res.FromDate.toString("dd/MM/yyyy").substring(0, 10);
    }
    if(res.ToDate != undefined || res.ToDate != null)
    {
      this.applyLeave.ToDate = res.ToDate.toString("dd/MM/yyyy").substring(0, 10);
    }
    if(res.HalfDayDate != undefined || res.HalfDayDate != null)
    {
      this.applyLeave.HalfDayDate = res.HalfDayDate.toString("dd/MM/yyyy").substring(0, 10);
      this.applyLeave.HalfDay = res.HalfDay;
    }
    this.applyLeave.Reason = res.Reason;
    this.applyLeave.Id = res.Id;
    this.applyLeave.NoOfDays = res.NoOfDays;
  }

  applyLeaveOperation(): void {
    if (this.id == undefined) {
      this.applyLeaveService.applyLeaveSave(this.applyLeave).subscribe(res => this.apiResponse(res));
    }
    else {
      this.applyLeaveService.updateLeave(this.applyLeave, this.id).subscribe(res => this.apiResponse(res));
    }
  }

  apiResponse(res) {
    if (res == "success")
    {
      Swal.fire(
        'Success',
        'Your Application Submitted Successfully.',
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
  }

  onValueBlur(ctrlValue) {
    let noOfDays: number = 0;
    
    if(this.applyLeave.FromDate != undefined && this.applyLeave.ToDate != undefined)
    {
      noOfDays = Date.parse(this.applyLeave.ToDate.toString()) - Date.parse(this.applyLeave.FromDate.toString());
      noOfDays = (noOfDays / (3600 * 1000 * 24)) + 1;
      if(this.applyLeave.HalfDay == true && (this.applyLeave.ToDate == this.applyLeave.HalfDayDate))
      {
        noOfDays = noOfDays - .5;
      }
      else if(this.applyLeave.HalfDay == true && (this.applyLeave.FromDate == this.applyLeave.HalfDayDate))
      {
        noOfDays = noOfDays - .5;
      }
      else if(this.applyLeave.HalfDay == true)
      {
        noOfDays = noOfDays + .5;
      }
      this.applyLeave.NoOfDays = noOfDays;
    }
    else if(this.applyLeave.HalfDay == true)
    {
      noOfDays = .5;
      this.applyLeave.NoOfDays = noOfDays;
    }
  }

  validateData(): boolean {
    let flag = true;

    document.getElementById('divFromDate').classList.remove('error');
    document.getElementById('divToDate').classList.remove('error');
    document.getElementById('divHalfDayDate').classList.remove('error');
    document.getElementById('divNoOfDay').classList.remove('error');
    document.getElementById('divReason').classList.remove('error');

    if(this.applyLeave.FromDate == undefined && this.applyLeave.ToDate != undefined)
    {
      document.getElementById('divFromDate').classList.add('error');
      flag = false;
    }
    if(this.applyLeave.FromDate != undefined && this.applyLeave.ToDate == undefined)
    {
      document.getElementById('divToDate').classList.add('error');
      flag = false;
    }
    if(this.applyLeave.FromDate != undefined && this.applyLeave.ToDate != undefined)
    {
      if(this.applyLeave.FromDate > this.applyLeave.ToDate)
      {
        document.getElementById('divFromDate').classList.add('error');
        flag = false;
      }
    }
    if(this.applyLeave.HalfDay == true)
    {
      if(this.applyLeave.HalfDayDate == undefined)
      {
        document.getElementById('divHalfDayDate').classList.add('error');
        flag = false;
      }
      else if(this.applyLeave.FromDate != undefined && this.applyLeave.ToDate != undefined)
      {
        if(this.applyLeave.FromDate > this.applyLeave.HalfDayDate)
        {
          document.getElementById('divHalfDayDate').classList.add('error');
          flag = false;
        }
        else if(this.applyLeave.ToDate < this.applyLeave.HalfDayDate)
        {
          document.getElementById('divHalfDayDate').classList.add('error');
          flag = false;
        }
      }
    }
    if(this.applyLeave.HalfDay == false)
    {
      if(this.applyLeave.HalfDayDate != undefined)
      {
        document.getElementById('divHalfDayDate').classList.add('error');
        flag = false;
      }
    }
    if(this.applyLeave.NoOfDays == 0)
    {
      document.getElementById('divNoOfDay').classList.add('error');
      flag = false;
    }
    if(this.applyLeave.Reason == '')
    {
      document.getElementById('divReason').classList.add('error');
      flag = false;
    }

    return flag;
  }
}
