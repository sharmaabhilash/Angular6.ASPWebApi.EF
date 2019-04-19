using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeavePortalAPI.BusinessLogic.Home
{
    public class CommonClassBO
    {
        public List<int> GetPageNoList(int totalPages)
        {
            List<int> pgeNolist = new List<int>();

            for (int i = 0; i < totalPages; i++)
            {
                int pageNumber = i + 1;

                //if (pageNo == (i + 1))
                //{
                //    pgeNolist.Add(pageNumber);
                //}
                //else
                //{
                    pgeNolist.Add(pageNumber);
                //}
            }
            return pgeNolist;
        }
    }
}