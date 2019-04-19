using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace EmployeeLeavePortalAPI.BusinessLogic.Home
{
    public class ValidationsBO
    {
        public void TextFieldsValidation(string value, int length)
        {
            try
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new Exception("Please Fill All Mandatory Details.");
                }
                if(value.Length > length)
                {
                    throw new Exception("");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void EmailFieldValidation(string value)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(value);
            }
            catch
            {
                throw new Exception("Please Enter Valid Email Address.");
            }
        }

        public void PasswordValidation(string value)
        {
            try
            {
                if(value.Length < 8)
                {
                    throw new Exception("Password Should Be Minimum 8 Character Long");
                }
                if(value.Length > 25)
                {
                    throw new Exception("Password Should Be Maximum 25 Character Long");
                }

                Regex regex = new Regex("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!\"#$%&'()*+,-./<=>@\\^_`{|}~])(?=\\S+$).{8,25})");

                if (!regex.IsMatch(value))
                {
                    throw new Exception("Password Should Contain At Least One Lower Case Letter, One Upper Case Letter, One Number, One Special Character And No Spaces");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DateValidation(DateTime fromDate, DateTime toDate)
        {
            try
            {
                if(fromDate > toDate)
                {
                    throw new Exception("Please Enter Valid Date.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void NullDateValidation(DateTime? date)
        {
            try
            {
                if(date == null)
                {
                    throw new Exception("Please Enter Valid Date.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}