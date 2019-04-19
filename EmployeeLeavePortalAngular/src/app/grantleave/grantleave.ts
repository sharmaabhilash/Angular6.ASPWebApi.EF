export class GrantLeave {
    AppliedBy: string;
    NoOfDays: number;
    FromDate: Date;
    ToDate: Date;
    HalfDay: string;
    HalfDayDate: Date;
    Reason: string;
    Approved: string;
    DepartmentId: number;
    LeaveUniqueId: string;
    UserId: number;
    ApplyLeaveId: number;

    constructor() {
        this.AppliedBy = '';
        this.NoOfDays = 0;
        this.HalfDay = '';
        this.Reason = '';
        this.Approved = '';
        this.DepartmentId = 0;
        this.LeaveUniqueId = '';
        this.UserId = 0;
        this.ApplyLeaveId = 0;
    }
}