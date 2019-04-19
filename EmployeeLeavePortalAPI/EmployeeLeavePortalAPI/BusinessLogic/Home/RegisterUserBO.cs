using Cryptography;
using EmployeeLeavePortalAPI.DataAccessLogic.Home;
using EmployeeLeavePortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeavePortalAPI.BusinessLogic.Home
{
    public class RegisterUserBO
    {
        private RegisterUserDAO objRegisterUserDAO;

        public RegisterUserBO()
        {
            objRegisterUserDAO = new RegisterUserDAO();
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
                listPositionDetails = objRegisterUserDAO.GetPositionDetails();
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
                listDepartmentDetails = objRegisterUserDAO.GetDepartmentDetails();
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
                EncryptionDecryption encryptionDecryption = new EncryptionDecryption();
                Guid UniqueId = Guid.NewGuid();

                objRegisterUser.UniqueId = Convert.ToString(UniqueId);
                objRegisterUser.Password = encryptionDecryption.Encrypt(objRegisterUser.Password);

                int affectedRow = objRegisterUserDAO.InsertRegisterUser(objRegisterUser);

                return affectedRow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}