using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BlalApi.BusinessModels
{
    public static class UserLogin
    {
        public static string userlogin(string UserName, string Password)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" SELECT DISTINCT fl.UserName ");
            sb.AppendLine(" FROM f_login fl ");
            sb.AppendLine(" INNER JOIN employee_master em ON fl.EmployeeID = em.Employee_ID ");
            sb.AppendLine(" INNER JOIN employee_hospital eh ON em.Employee_ID = eh.Employee_ID ");
            sb.AppendLine(" INNER JOIN centre_master cm ON cm.CentreID=fl.CentreID");
            sb.AppendLine(" INNER JOIN f_rolemaster rm ON rm.ID=fl.RoleID");
            sb.AppendLine(" WHERE cm.IsActive = 1 AND em.IsActive=1  AND PASSWORD(LOWER(fl.UserName)) = PASSWORD(LOWER('" + UserName + "')) ");
            sb.AppendLine(" AND PASSWORD(LOWER(fl.Password)) = PASSWORD(LOWER('" + Password + "'))  ");
            sb.AppendLine(" ORDER BY fl.isDefault DESC");
            return sb.ToString();
        }
        public static string userbyUserName(string UserName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" SELECT DISTINCT fl.UserName ");
            sb.AppendLine(" FROM f_login fl ");
            sb.AppendLine(" INNER JOIN employee_master em ON fl.EmployeeID = em.Employee_ID ");
            sb.AppendLine(" INNER JOIN f_rolemaster rm ON rm.ID=fl.RoleID");
            sb.AppendLine(" WHERE em.IsActive=1  AND PASSWORD(LOWER(fl.UserName)) = PASSWORD(LOWER('" + UserName + "')) ");
            return sb.ToString();
        }
    }
}