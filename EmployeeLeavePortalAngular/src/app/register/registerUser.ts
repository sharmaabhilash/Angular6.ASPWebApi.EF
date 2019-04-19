export class RegisterUser {
    Id: number;
    Name: string;
    EmailId: string;
    Password: string;
    DateOfJoining: string;
    PositionId: Number;
    DepartmentId: Number;
    ApprovalPermission: string;
    FileName: string;

    constructor() {
        this.Id = 0;
        this.Name = '';
        this.EmailId = '';
        this.Password = '';
        this.DateOfJoining = '';
        this.PositionId = 0;
        this.DepartmentId = 0;
        this.ApprovalPermission = "No";
        this.FileName = "";
    }
}