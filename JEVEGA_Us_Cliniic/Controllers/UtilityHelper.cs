using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace JEVEGA_Us_Cliniic.Controllers
{
    public class UtilityHelper : Controller
    {

        public string UnauthorizedAccessMessage()
        {
            string uaccess_msg = "Unauthorized Access! You are not allowed to access admin pages!";
            return uaccess_msg;
        }

        public bool CheckAdminAccess(string user_type)
        {
            if (user_type != "1")   //-- Admin user ... 
            {
                return false;
            }
            else {
                return true;
            }

        } //-- 

        public void InsertLogAudit(string session_username)
        {
            string local_machineName = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            string localIP;
            localIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            DateTime log_datetime = DateTime.Now;

            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                string command_text = "Insert Into AuditLog(Username, LogDate, MachineName, IpAddress) ";
                command_text += "Values (@user, @logdate, @machine, @ip_add)";
                sqlCmd.CommandText = command_text;

                sqlCmd.Parameters.AddWithValue("@user", session_username);
                sqlCmd.Parameters.AddWithValue("@logdate", log_datetime);
                sqlCmd.Parameters.AddWithValue("@machine", local_machineName);
                sqlCmd.Parameters.AddWithValue("@ip_add", localIP);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                int rowaffected = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

        } //--

    }
}