using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

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

        public string getGenderDefinition(string gender_code)
        {
            string gender_definition = "";
            switch (gender_code)
            {
                case "F":
                    gender_definition = "Female"; break;
                case "M":
                    gender_definition = "Male"; break;
            }

            return gender_definition;

        } //-- 

        public string getStatusDefinition(string status_code)
        {
            string status_definition = "";
            switch (status_code)
            {
                case "S":
                    status_definition = "Single"; break;
                case "M":
                    status_definition = "Married"; break;
                case "W":
                    status_definition = "Widow"; break;
            }

            return status_definition;
        } //--

        public void SendMailtoUser(string email_subject, string email_body, string email_addressTo)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("kaugisaddmin@gmail.com", "Crv@UE8903510");

            string emailAddrFrom = "kaugisaddmin@gmail.com";

            MailMessage mm = new MailMessage(emailAddrFrom, email_addressTo, email_subject, email_body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);

        } //**..

    }
}