using EmployeeLeavePortalAPI.BusinessLogic.Home;
using EmployeeLeavePortalAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace EmployeeLeavePortalAPI.Controllers.Home
{
    [Authorize]
    [RoutePrefix("api/leave")]
    public class LeaveFunctionalityController : ApiController
    {
        [HttpPost]
        [Route("applyleave")]
        public string ApplyLeave([FromBody] Tbl_ApplyLeave applyLeave, string id)
        {
            try
            {
                ApplyLeaveBO applyLeaveBO = new ApplyLeaveBO();
                int affectedRow = applyLeaveBO.ApplyForLeave(applyLeave, id);
                if (affectedRow > 0)
                    return "success";
                else
                    return "failed";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPut]
        [Route("updateappliedleave")]
        public string UpdateAppliedLeave([FromBody] Tbl_ApplyLeave applyLeave, string id)
        {
            try
            {
                ApplyLeaveBO applyLeaveBO = new ApplyLeaveBO();
                int affectedRow = applyLeaveBO.UpdateLeave(applyLeave, id);
                if (affectedRow > 0)
                    return "success";
                else
                    return "You Cannot Update Approved Leaves.";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpDelete]
        [Route("deleteappliedleave")]
        public string DeleteAppliedLeave(string leaveId)
        {
            try
            {
                ApplyLeaveBO applyLeaveBO = new ApplyLeaveBO();
                int affectedRow = applyLeaveBO.DeleteLeave(leaveId);
                if (affectedRow > 0)
                    return "success";
                else
                    return "You Cannot Delete Approved Leaves.";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        [Route("getuserleave")]
        public Tbl_ApplyLeave GetUserLeave(string leaveId)
        {
            Tbl_ApplyLeave _ApplyLeave = new Tbl_ApplyLeave();
            try
            {
                ApplyLeaveBO applyLeaveBO = new ApplyLeaveBO();
                _ApplyLeave = applyLeaveBO.GetUserLeave(leaveId);
                return _ApplyLeave;
            }
            catch(Exception ex)
            {
                return _ApplyLeave;
            }
        }

        [HttpGet]
        [Route("getuserleaves")]
        public Hashtable GetUserLeaves(string id, int pageNo)
        {
            Hashtable hashtable = new Hashtable();
            try
            {
                ApplyLeaveBO applyLeaveBO = new ApplyLeaveBO();
                hashtable = applyLeaveBO.GetAppliedLeaveByUser(id, pageNo);
                return hashtable;
            }
            catch
            {
                return hashtable;
            }
        }

        [HttpGet]
        [Route("getallleaves")]
        public Hashtable GetAllLeaves(string id, int pageNo)
        {
            Hashtable hashtable = new Hashtable();
            try
            {
                GrantLeavesBO grantLeavesBO = new GrantLeavesBO();
                hashtable = grantLeavesBO.GetAllAppliedLeaveData(id, pageNo);
                return hashtable;
            }
            catch
            {
                return hashtable;
            }
        }

        [HttpPut]
        [Route("approveleave")]
        public string ApproveLeave(string userId, string leaveId)
        {
            try
            {
                GrantLeavesBO grantLeavesBO = new GrantLeavesBO();
                int affectedRow = grantLeavesBO.ApproveLeave(userId, leaveId);
                if (affectedRow > 0)
                    return "success";
                else
                    return "failed";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPut]
        [Route("unapproveleave")]
        public string UnapproveLeave(string userId, string leaveId)
        {
            try
            {
                GrantLeavesBO grantLeavesBO = new GrantLeavesBO();
                int affectedRow = grantLeavesBO.UnapproveLeave(userId, leaveId);
                if (affectedRow > 0)
                    return "success";
                else
                    return "failed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
