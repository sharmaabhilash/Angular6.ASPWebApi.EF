using EmployeeLeavePortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeLeavePortalAPI.DataAccessLogic.Home
{
    public class LoginUserDAO
    {
        private EmployeeLeavePortalEntities employeeLeavePortalEntities;
        public LoginUserDAO()
        {
            employeeLeavePortalEntities = new EmployeeLeavePortalEntities();
        }

        /// <summary>
        /// Check User Credential For Login.
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CheckUserCredential(string emailId, string password)
        {
            try
            {
                int? CheckValidUser = 0;
                ObjectParameter UniqueId = new ObjectParameter("UniqueId", typeof(string));
                ObjectParameter ApprovalPermission = new ObjectParameter("ApprovalPermission", typeof(string));
                ObjectParameter UserName = new ObjectParameter("UserName", typeof(string));
                ObjectParameter ImageName = new ObjectParameter("ImageName", typeof(string));
                CheckValidUser = employeeLeavePortalEntities.CheckForLogin(emailId, password, UniqueId, ApprovalPermission, UserName, ImageName).FirstOrDefault();
                string UniqueIdValue = Convert.ToString(UniqueId.Value);
                string ApprovalPermissionValue = Convert.ToString(ApprovalPermission.Value);
                string UserNameValue = Convert.ToString(UserName.Value);
                string ImageNameValue = Convert.ToString(ImageName.Value);
                string ReturnValue = UniqueIdValue + ":" + ApprovalPermissionValue + ":" + UserNameValue + ":" + ImageNameValue;
                if (CheckValidUser != null || CheckValidUser > 0)
                {
                    return ReturnValue;
                }
                else
                {
                    throw new Exception("Not a valid user.");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check User Credential To Generate Token.
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Tbl_RegisterUser CheckUserCredentialToken(string emailId, string password)
        {
            try
            {
                Tbl_RegisterUser objRegisterUser = employeeLeavePortalEntities.Tbl_RegisterUser.SqlQuery("SELECT * FROM Tbl_RegisterUser WHERE EmailId = '" + emailId + "' AND Password = '" + password + "'").FirstOrDefault();
                return objRegisterUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}