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
            client.Credentials = new System.Net.NetworkCredential("crisvega71@gmail.com", "Crv@UEM8903510");

            string emailAddrFrom = "crisvega71@gmail.com";

            MailMessage mm = new MailMessage(emailAddrFrom, email_addressTo, email_subject, email_body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);

        } //**..

        public string SendSMTPmail(string email_subject, string email_body, string email_addressTo, string email_addressFrom, string email_fromPwd)
        {
            string result_message = "";
            string CCemailAdd = "cdvegajr@gmail.com";

            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            m.From = new MailAddress(email_addressFrom);
            m.To.Add(email_addressTo);
            m.CC.Add(CCemailAdd);
            m.Subject = email_subject;
            m.Body = email_body;

            sc.Host = "mail.jevegausdiagnostic.com";

            string str1 = "gmail.com";
            string str2 = email_addressFrom.ToLower();
            if (str2.Contains(str1))
            {
                try
                {
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential(email_addressFrom, email_fromPwd);
                    sc.EnableSsl = true;
                    sc.Send(m);
                    result_message = "Email Send successfully";
                }
                catch (Exception ex)
                {
                    result_message = "* Please double check the From Address and Password ...";
                    throw ex;
                }
            }
            else
            {
                try
                {
                    sc.Port = 25;
                    sc.Credentials = new System.Net.NetworkCredential(email_addressFrom, email_fromPwd);
                    sc.EnableSsl = false;
                    sc.Send(m);
                    result_message = "Email Send successfully";
                }
                catch (Exception ex)
                {
                    result_message = "* Please double check the From Address and Password ...";
                    throw ex;
                }
            }

            return (result_message);
        } //--


    }
}