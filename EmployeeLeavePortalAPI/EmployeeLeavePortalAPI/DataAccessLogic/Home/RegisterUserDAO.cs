using EmployeeLeavePortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeavePortalAPI.DataAccessLogic.Home
{
    public class RegisterUserDAO
    {
        private EmployeeLeavePortalEntities objEmployeeLeavePortalEntity;

        public RegisterUserDAO()
        {
            objEmployeeLeavePortalEntity = new EmployeeLeavePortalEntities();
        }

        /// <summary>
        /// Get Position Details For Registration Page
        /// </summary>
        /// <returns></returns>
        public List<USP_GetPositionDetails_Result> GetPositionDetails()
        {
            try
            {
                List<USP_GetPositionDetails_Result> listPositionDetails = new List<USP_GetPositionDetails_Result>();
                listPositionDetails = objEmployeeLeavePortalEntity.GetPositionDetails().ToList();
                return listPositionDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Department Details For Registration Page
        /// </summary>
        /// <returns></returns>
        public List<USP_GetDepartmentDetails_Result> GetDepartmentDetails()
        {
            try
            {
                List<USP_GetDepartmentDetails_Result> listDepartmentDetails = new List<USP_GetDepartmentDetails_Result>();
                listDepartmentDetails = objEmployeeLeavePortalEntity.GetDepartmentDetails().ToList();
                return listDepartmentDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save User's Data
        /// </summary>
        /// <param name="objRegisterUser"></param>
        public int InsertRegisterUser(Tbl_RegisterUser objRegisterUser)
        {
            try
            {
                int affectedRow = objEmployeeLeavePortalEntity.OperationRegisterUser(id: objRegisterUser.Id,name: objRegisterUser.Name, emailId: objRegisterUser.EmailId, password: objRegisterUser.Password, dateOfJoining: objRegisterUser.DateOfJoining, positionId: objRegisterUser.PositionId, departmentId: objRegisterUser.DepartmentId, approvalPermission: objRegisterUser.ApprovalPermission, uniqueId: objRegisterUser.UniqueId, fileName: objRegisterUser.FileName);

                return affectedRow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}