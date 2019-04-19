export class ApplyLeave {
    Id: number;
    ApplyDate: Date;
    FromDate: Date;
    ToDate: Date;
    HalfDay: boolean;
    HalfDayDate: Date;
    Reason: string;
    NoOfDays: number;
    AppliedBy: string;

    constructor() {
        this.Id = 0;
        this.ApplyDate = new Date();
        this.HalfDay = false;
        this.Reason = '';
        this.NoOfDays = 0;
        this.AppliedBy = '';
    }
}