//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeLeavePortalAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VW_GetAllAppliedLeave
    {
        public string AppiedBy { get; set; }
        public Nullable<decimal> NoOfDays { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string HalfDay { get; set; }
        public Nullable<System.DateTime> HalfDayDate { get; set; }
        public string Reason { get; set; }
        public string Approved { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string LeaveUniqueId { get; set; }
        public int UserId { get; set; }
        public long ApplyLeaveId { get; set; }
    }
}
