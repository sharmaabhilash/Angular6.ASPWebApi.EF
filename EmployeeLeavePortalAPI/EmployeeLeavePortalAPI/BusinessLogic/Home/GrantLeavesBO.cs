using EmployeeLeavePortalAPI.DataAccessLogic.Home;
using EmployeeLeavePortalAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeavePortalAPI.BusinessLogic.Home
{
    public class GrantLeavesBO
    {
        private GrantLeavesDAO grantLeavesDAO;

        public GrantLeavesBO()
        {
            grantLeavesDAO = new GrantLeavesDAO();
        }

        /// <summary>
        /// Get All Applied Leaves By Users On The Basis Of Their Department For Approve/Unapprove.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public Hashtable GetAllAppliedLeaveData(string id, int pageNo)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                List<int> PageNoList = new List<int>();
                CommonClassBO commonClassBO = new CommonClassBO();

                int recordPerPage = 10;
                int totalPages = 0;
                int totalRecords = grantLeavesDAO.GetTotalAppliedLeaves(id);
                bool disablePrevBtn = false;
                bool disableNextBtn = false;

                if(totalRecords > 0)
                {
                    if(totalRecords % recordPerPage == 0)
                    {
                        totalPages = totalRecords / recordPerPage;
                    }
                    else
                    {
                        totalPages = 1 + (totalRecords / recordPerPage);
                    }
                    PageNoList = commonClassBO.GetPageNoList(totalPages);
                }
                if (totalRecords == 0 || totalRecords <= recordPerPage)
                {
                    disableNextBtn = true;
                    disablePrevBtn = true;
                }
                else if (pageNo == 1)
                {
                    disablePrevBtn = true;
                    disableNextBtn = false;
                }
                else if(pageNo == totalPages)
                {
                    disablePrevBtn = false;
                    disableNextBtn = true;
                }

                List<VW_GetAllAppliedLeave> ListGetAllAppliedLeaves = grantLeavesDAO.GetAllAppliedLeaveData(id, pageNo, recordPerPage);

                foreach (VW_GetAllAppliedLeave list in ListGetAllAppliedLeaves)
                {
                    list.HalfDay = list.HalfDay == "true" ? "Yes" : "No";
                }

                hashtable.Add("AllLeaveData", ListGetAllAppliedLeaves);
                hashtable.Add("TotalLeaveList", PageNoList);
                hashtable.Add("DisablePrevBtn", disablePrevBtn);
                hashtable.Add("DisableNextBtn", disableNextBtn);
                hashtable.Add("PageNo", pageNo);
                hashtable.Add("RecordPerPage", recordPerPage);
                hashtable.Add("TotalLeaves", totalRecords);

                return hashtable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Approve Leaves.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="leaveId"></param>
        /// <returns></returns>
        public int ApproveLeave(string userId, string leaveId)
        {
            try
            {
                int affectedRows = grantLeavesDAO.GrantUngrantLeave(userId, leaveId, "A");
                return affectedRows;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unapprove Leaves.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="leaveId"></param>
        /// <returns></returns>
        public int UnapproveLeave(string userId, string leaveId)
        {
            try
            {
                int affectedRows = grantLeavesDAO.GrantUngrantLeave(userId, leaveId, "U");
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}