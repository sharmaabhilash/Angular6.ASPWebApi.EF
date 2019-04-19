using EmployeeLeavePortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeavePortalAPI.DataAccessLogic.Home
{
    public class GrantLeavesDAO
    {
        private EmployeeLeavePortalEntities objEmployeeLeavePortalEntity;

        public GrantLeavesDAO()
        {
            objEmployeeLeavePortalEntity = new EmployeeLeavePortalEntities();
        }
        
        /// <summary>
        /// Get All Applied Leaves By Users On The Basis Of Their Department For Approve/Unapprove.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public List<VW_GetAllAppliedLeave> GetAllAppliedLeaveData(string id, int pageNo, int recordsPerPage)
        {
            try
            {
                int startRow = 1 + ((pageNo - 1) * recordsPerPage);
                int endRow = pageNo * recordsPerPage;
                string Query = string.Format(@"SELECT TOP {0} * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY LEAVEINFO.ApplyLeaveId DESC) AS Row FROM VW_GetAllAppliedLeave LEAVEINFO WHERE LEAVEINFO.DepartmentId = (Select DepartmentId From Tbl_RegisterUser Where UniqueId = '{1}')) A WHERE Row BETWEEN {2} and {3} ORDER BY A.ApplyLeaveId DESC", endRow, id, startRow, endRow);
                List<VW_GetAllAppliedLeave> ListGetAllAppliedLeave = objEmployeeLeavePortalEntity.VW_GetAllAppliedLeave.SqlQuery(Query).ToList();
                return ListGetAllAppliedLeave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Total Count Of Applied Leaves.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetTotalAppliedLeaves(string id)
        {
            try
            {
                string Query = string.Format(@"SELECT * FROM VW_GetAllAppliedLeave WHERE DepartmentId = (Select DepartmentId From Tbl_RegisterUser Where UniqueId = '{0}')", id);
                List<VW_GetAllAppliedLeave> ListGetAllAppliedLeave = objEmployeeLeavePortalEntity.VW_GetAllAppliedLeave.SqlQuery(Query).ToList();
                int TotalRecords = ListGetAllAppliedLeave.Count;
                return TotalRecords;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Approve/Unapprove Leave.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="leaveId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int GrantUngrantLeave(string userId, string leaveId, string status)
        {
            try
            {
                int affectedRows = objEmployeeLeavePortalEntity.ApproveUnapproveLeave(leaveId, status, userId);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}