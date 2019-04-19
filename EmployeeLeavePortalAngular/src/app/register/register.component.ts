import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from './department';
import { RegisterUser } from './registerUser';
import { RegisterService } from './register.service';
import { Position } from './position';
import { ApiUrls } from '../apiurls';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [RegisterService, ApiUrls]
})
export class RegisterComponent {

  positionList: Observable<Position[]>;
  positions: Position[];
  positionId: Number;
  fileName: string = '';

  departmentList: Observable<Department[]>;
  departments: Department[];
  departmentId: Number;

  register = new RegisterUser;

  constructor(private registerService: RegisterService) {
      this.getPositionList();
      this.getDepartmentList();
  }

  getPositionList(): void {
      this.positionList = this.registerService.getPositionData();
      this.positionList.subscribe(positions => this.positions = positions);
  }

  getDepartmentList(): void {
      this.departmentList = this.registerService.getDepartmentData();
      this.departmentList.subscribe(departments => this.departments = departments);
  }

  selectPosition() {
      alert(this.positionId);
  }

  registerUserOperation(): void {
    // Swal.fire({
    //   title: 'Are you sure?',
    //   text: 'You will not be able to recover this imaginary file!',
    //   type: 'warning',
    //   showCancelButton: true,
    //   confirmButtonText: 'Yes, delete it!',
    //   cancelButtonText: 'No, keep it'
    // }).then((result) => {
    //   if (result.value) {
    //     Swal.fire(
    //       'Deleted!',
    //       'Your imaginary file has been deleted.',
    //       'success'
    //     )
    //   // For more information about handling dismissals please visit
    //   // https://sweetalert2.github.io/#handling-dismissals
    //   } else if (result.dismiss === Swal.DismissReason.cancel) {
    //     Swal.fire(
    //       'Cancelled',
    //       'Your imaginary file is safe :)',
    //       'error'
    //     )
    //   }
    // })
    this.register.FileName = this.fileName;
    this.registerService.saveRegisterUser(this.register).subscribe(res => this.apiResponse(res));
  }

  apiResponse(res) {
    if(res == "success") {
      Swal.fire(
        'Success',
        'You are successfully registered.',
        'success'
      )
    }
    else {
      Swal.fire(
        'Failed',
        'Oops! Something went wrong',
        'error'
      )
    }
  }

  validateData(): boolean {
    let flag = true;

    return flag;
  }

  fileChange(event) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      let file: File = fileList[0];
      let formData: FormData = new FormData();
      formData.append('uploadFile', file, file.name);
      this.registerService.uploadFile(formData).subscribe(res => this.fileName = res);
    }
  }
}
