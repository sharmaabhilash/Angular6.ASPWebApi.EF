using EmployeeLeavePortalAPI.BusinessLogic.Home;
using EmployeeLeavePortalAPI.Models;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace EmployeeLeavePortalAPI.Controllers.Home
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Register User Functionality
        [HttpGet]
        [Route("position")]
        public List<USP_GetPositionDetails_Result> PositionDetails()
        {
            List<USP_GetPositionDetails_Result> listPositionDetails = new List<USP_GetPositionDetails_Result>();
            try
            {
                RegisterUserBO objRegisterUserBO = new RegisterUserBO();
                listPositionDetails = objRegisterUserBO.GetPositionDetails();
            }
            catch (Exception ex)
            {
                log.Error("Position Details: " + ex.Message);
            }
            return listPositionDetails;
        }

        [HttpGet]
        [Route("department")]
        public List<USP_GetDepartmentDetails_Result> DepartmentDetails()
        {
            List<USP_GetDepartmentDetails_Result> listDepartmentDetails = new List<USP_GetDepartmentDetails_Result>();
            try
            {
                RegisterUserBO objRegisterUserBO = new RegisterUserBO();
                listDepartmentDetails = objRegisterUserBO.GetDepartmentDetails();
            }
            catch (Exception ex)
            {
                log.Error("Department Details: " + ex.Message);
            }
            return listDepartmentDetails;
        }

        [HttpPost]
        [Route("saveregisteruser")]
        public string SaveRegisterUser([FromBody] Tbl_RegisterUser objRegisterUser)
        {
            try
            {
                RegisterUserBO objRegisterUserBO = new RegisterUserBO();
                CopyFile(objRegisterUser.FileName);
                int affectedRow = objRegisterUserBO.InsertRegisterUser(objRegisterUser);
                if (affectedRow > 0)
                {
                    log.Info("Save Register User: " + objRegisterUser.Name + " is regestered successfully.");
                    return "success";
                }
                else
                {
                    log.Info("Save Register User: " + objRegisterUser.Name + " is not regestered successfully.");
                    return "failed";
                }
            }
            catch (Exception ex)
            {
                log.Error("Save Register User: " + objRegisterUser.Name + ", " + ex.Message);
                return ex.Message;
            }
        }

        public void CopyFile(string fileName)
        {
            try
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/UploadFile");
                string tempDirectoryPath = HttpContext.Current.Server.MapPath("~/TempUploadFile");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                File.Move(tempDirectoryPath + "/" + fileName, directoryPath + "/" + fileName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("uploadfile")]
        public string UploadFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                if (httpRequest.Files.Count > 0)
                {
                    string tempDirectoryPath = HttpContext.Current.Server.MapPath("~/TempUploadFile");
                    if(!Directory.Exists(tempDirectoryPath))
                    {
                        Directory.CreateDirectory(tempDirectoryPath);
                    }
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        string[] arrFileName = postedFile.FileName.Split('.');
                        int length = arrFileName.Length;
                        string extension = arrFileName[length - 1];
                        fileName = fileName + "." + extension;
                        var filePath = HttpContext.Current.Server.MapPath("~/TempUploadFile/" + fileName);
                        postedFile.SaveAs(filePath);
                    }
                }
                return fileName;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Login User Data
        [HttpPost]
        [Route("loginuser")]
        [Authorize]
        public Hashtable LoginUser([FromBody] Tbl_RegisterUser objRegisterUser)
        {
            Hashtable HashTable = new Hashtable();
            try
            {
                LoginUserBO loginUserBO = new LoginUserBO();
                string ReturnValue = loginUserBO.CheckUserCredential(objRegisterUser.EmailId, objRegisterUser.Password);
                string UniqueId = ReturnValue.Split(':')[0];
                string ApprovalPermission = ReturnValue.Split(':')[1];
                string UserName = ReturnValue.Split(':')[2];
                string ImageName = ReturnValue.Split(':')[3];
                HashTable.Add("status", "success");
                HashTable.Add("id", UniqueId);
                HashTable.Add("approvalPermission", ApprovalPermission);
                HashTable.Add("userName", UserName);
                HashTable.Add("imageName", ImageName);
                log.Info("Login User: " + objRegisterUser.EmailId + " has been logged in.");
            }
            catch (Exception ex)
            {
                log.Error("Login User: " + ex.Message);
                HashTable.Add("status", ex.Message);
                HashTable.Add("id", "");
                HashTable.Add("approvalPermission", "");
            }
            return HashTable;
        }
        #endregion
    }
}
