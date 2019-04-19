using EmployeeLeavePortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeavePortalAPI.DataAccessLogic.Home
{
    public class ApplyLeaveDAO
    {
        private EmployeeLeavePortalEntities objEmployeeLeavePortalEntity;

        public ApplyLeaveDAO()
        {
            objEmployeeLeavePortalEntity = new EmployeeLeavePortalEntities();
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
                int affectedRows = objEmployeeLeavePortalEntity.OperationApplyLeave(id: applyLeave.Id, appliedBy: id, applyDate: applyLeave.ApplyDate, fromDate: applyLeave.FromDate, toDate: applyLeave.ToDate, noOfDays: applyLeave.NoOfDays, halfDay: applyLeave.HalfDay, halfDayDate: applyLeave.HalfDayDate, reason: applyLeave.Reason, mode: 1, uniqueId: applyLeave.UniqueId);
                return affectedRows;
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
                int affectedRows = objEmployeeLeavePortalEntity.OperationApplyLeave(id: applyLeave.Id, appliedBy: "", applyDate: applyLeave.ApplyDate, fromDate: applyLeave.FromDate, toDate: applyLeave.ToDate, noOfDays: applyLeave.NoOfDays, halfDay: applyLeave.HalfDay, halfDayDate: applyLeave.HalfDayDate, reason: applyLeave.Reason, mode: 2, uniqueId: id);
                return affectedRows;
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
                int affectedRows = objEmployeeLeavePortalEntity.OperationApplyLeave(id: 0, appliedBy: "", applyDate: null, fromDate: null, toDate: null, noOfDays: 0, halfDay: null, halfDayDate: null, reason: null, mode: 3, uniqueId: leaveId);
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
                string Query = string.Format(@"SELECT * FROM Tbl_ApplyLeave WHERE UniqueId = '{0}'", id);

                _ApplyLeave = objEmployeeLeavePortalEntity.Tbl_ApplyLeave.SqlQuery(Query).First();
                return _ApplyLeave;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all the applied leave for logged in user to edit or delete.
        /// </summary>
        /// <param name="id">User Unique Id</param>
        public List<VW_GetAppliedLeaveInfo> GetAppliedLeaveByUser(string id, int pageNo, int recordsPerPage)
        {
            try
            {
                int startRow = 1 + ((pageNo - 1) * recordsPerPage);
                int endRow = pageNo * recordsPerPage;
                string Query = string.Format(@"SELECT TOP {0} * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY LEAVEINFO.ApplyLeaveId DESC) AS Row FROM VW_GetAppliedLeaveInfo LEAVEINFO WHERE LEAVEINFO.UserUniqueId = '{1}') A WHERE Row BETWEEN {2} and {3} ORDER BY A.ApplyLeaveId DESC", endRow, id, startRow, endRow);
                List<VW_GetAppliedLeaveInfo> ListGetAppliedLeaveInfo = objEmployeeLeavePortalEntity.VW_GetAppliedLeaveInfo.SqlQuery(Query).ToList();
                return ListGetAppliedLeaveInfo;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get total number of leaves applied by user on the basis of user id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetTotalLeaveByUser(string id)
        {
            try
            {
                int TotalAppliedLeaves = objEmployeeLeavePortalEntity.VW_GetAppliedLeaveInfo.Count(x => x.UserUniqueId == id);
                return TotalAppliedLeaves;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}