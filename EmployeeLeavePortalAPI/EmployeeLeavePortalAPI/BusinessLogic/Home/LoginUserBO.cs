using Cryptography;
using EmployeeLeavePortalAPI.DataAccessLogic.Home;
using EmployeeLeavePortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeavePortalAPI.BusinessLogic.Home
{
    public class LoginUserBO
    {
        private LoginUserDAO loginUserDAO;
        public LoginUserBO()
        {
            loginUserDAO = new LoginUserDAO();
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
                EncryptionDecryption encryptionDecryption = new EncryptionDecryption();
                password = encryptionDecryption.Encrypt(password);
                string ReturnValue = Convert.ToString(loginUserDAO.CheckUserCredential(emailId, password));
                return ReturnValue;
            }
            catch (Exception ex)
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
                EncryptionDecryption encryptionDecryption = new EncryptionDecryption();
                password = encryptionDecryption.Encrypt(password);
                Tbl_RegisterUser objRegisterUser = loginUserDAO.CheckUserCredentialToken(emailId, password);
                return objRegisterUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}