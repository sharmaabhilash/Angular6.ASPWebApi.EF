using EmployeeLeavePortalAPI.DataAccessLogic.Home;
using EmployeeLeavePortalAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeavePortalAPI.BusinessLogic.Home
{
    public class ApplyLeaveBO
    {
        private ApplyLeaveDAO applyLeaveDAO;

        public ApplyLeaveBO()
        {
            applyLeaveDAO = new ApplyLeaveDAO();
        }

        /// <summary>
        /// Apply For Leave.
        /// </summary>
        /// <param name="applyLeave"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ApplyForLeave(Tbl_ApplyLeave applyLeave, string id)
        {
            try
            {
                Guid uniqueId = Guid.NewGuid();
                applyLeave.UniqueId = uniqueId.ToString();
                applyLeave.ApplyDate = DateTime.Now;
                DateTime FromDate = Convert.ToDateTime(applyLeave.FromDate);
                DateTime ToDate = Convert.ToDateTime(applyLeave.ToDate);
                DateTime HalfDayDate = Convert.ToDateTime(applyLeave.HalfDayDate);

                TimeSpan timeSpan = ToDate.Subtract(FromDate);

                decimal NoOfDays = ((decimal)timeSpan.TotalDays) + 1;
                if (applyLeave.FromDate != null && applyLeave.ToDate != null)
                {
                    if (applyLeave.HalfDay == "true" && (ToDate == HalfDayDate))
                    {
                        NoOfDays = NoOfDays - (decimal)0.5;
                    }
                    else if (applyLeave.HalfDay == "true" && (FromDate == HalfDayDate))
                    {
                        NoOfDays = NoOfDays - (decimal)0.5;
                    }
                    else if (applyLeave.HalfDay == "true")
                    {
                        NoOfDays = NoOfDays + (decimal)0.5;
                    }
                }
                else if(applyLeave.HalfDay == "true")
                {
                    NoOfDays = (decimal)0.5;
                }
                applyLeave.NoOfDays = NoOfDays;

                ValidationsBO validationsBO = new ValidationsBO();
                validationsBO.TextFieldsValidation(applyLeave.Reason, 250);
                validationsBO.TextFieldsValidation(applyLeave.UniqueId, 100);
                
                if(applyLeave.HalfDay == "false")
                {
                    validationsBO.NullDateValidation(applyLeave.FromDate);
                    validationsBO.NullDateValidation(applyLeave.ToDate);
                }
                if(applyLeave.FromDate != null && applyLeave.ToDate != null)
                {
                    validationsBO.DateValidation(FromDate, ToDate);
                }
                if(applyLeave.HalfDay == "true")
                {
                    validationsBO.NullDateValidation(applyLeave.HalfDayDate);
                    if(applyLeave.FromDate != null)
                    {
                        validationsBO.DateValidation(FromDate, HalfDayDate);
                    }
                    if(applyLeave.ToDate != null)
                    {
                        validationsBO.DateValidation(ToDate, HalfDayDate);
                    }
                }
                if(applyLeave.NoOfDays <= 0)
                {
                    throw new Exception("Not Valid Number Of Days");
                }

                int affectedRow = applyLeaveDAO.ApplyForLeave(applyLeave, id);
                return affectedRow;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Leave Data.
        /// </summary>
        /// <param name="applyLeave"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateLeave(Tbl_ApplyLeave applyLeave, string id)
        {
            try
            {
                int affectedRow = applyLeaveDAO.UpdateLeave(applyLeave, id);
                return affectedRow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete Leave Data.
        /// </summary>
        /// <param name="leaveId"></param>
        /// <returns></returns>
        public int DeleteLeave(string leaveId)
        {
            try
            {
                int affectedRows = applyLeaveDAO.DeleteLeave(leaveId);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets Data Of Single Applied Leave.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tbl_ApplyLeave GetUserLeave(string id)
        {
            try
            {
                Tbl_ApplyLeave _ApplyLeave = new Tbl_ApplyLeave();

                _ApplyLeave = applyLeaveDAO.GetUserLeave(id);
                return _ApplyLeave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all the applied leave for logged in user to edit or delete.
        /// </summary>
        /// <param name="id">User Unique Id</param>
        public Hashtable GetAppliedLeaveByUser(string id, int _pageNo = 0)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                int TotalPages = 0;
                int RecordsPerPage = 10;
                bool DisablePrevBtn = false;
                bool DisableNextBtn = false;

                List<VW_GetAppliedLeaveInfo> ListGetAppliedLeaveInfo = applyLeaveDAO.GetAppliedLeaveByUser(id, _pageNo, RecordsPerPage);
                int TotalAppliedLeaves = applyLeaveDAO.GetTotalLeaveByUser(id);
                
                foreach (VW_GetAppliedLeaveInfo list in ListGetAppliedLeaveInfo)
                {
                    list.HalfDay = list.HalfDay == "true" ? "Yes" : "No";
                }
                
                CommonClassBO commonClassBO = new CommonClassBO();
                List<int> TotalLeaveList = new List<int>();

                if(TotalAppliedLeaves > 0)
                {
                    if(TotalAppliedLeaves % RecordsPerPage == 0)
                    {
                        TotalPages = TotalAppliedLeaves / RecordsPerPage;
                        TotalLeaveList = commonClassBO.GetPageNoList(TotalPages);
                    }
                    else if(TotalAppliedLeaves % RecordsPerPage != 0)
                    {
                        TotalPages = 1 + (TotalAppliedLeaves / RecordsPerPage);
                        TotalLeaveList = commonClassBO.GetPageNoList(TotalPages);
                    }
                }

                if (TotalAppliedLeaves == 0 || TotalAppliedLeaves <= RecordsPerPage)
                {
                    DisablePrevBtn = true;
                    DisableNextBtn = true;
                }
                else if (_pageNo == 1)
                {
                    DisablePrevBtn = true;
                    DisableNextBtn = false;
                }
                else if (_pageNo == TotalPages)
                {
                    DisablePrevBtn = false;
                    DisableNextBtn = true;
                }

                hashtable.Add("AllLeaveData", ListGetAppliedLeaveInfo);
                hashtable.Add("TotalLeaveList", TotalLeaveList);
                hashtable.Add("DisablePrevBtn", DisablePrevBtn);
                hashtable.Add("DisableNextBtn", DisableNextBtn);
                hashtable.Add("PageNo", _pageNo);
                hashtable.Add("RecordPerPage", RecordsPerPage);
                hashtable.Add("TotalLeaves", TotalAppliedLeaves);

                return hashtable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}