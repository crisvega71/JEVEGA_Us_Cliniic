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
            else
            {
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
            string CCemailAdd2 = "jev.ultrasound.services@gmail.com";

            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            m.From = new MailAddress(email_addressFrom);
            m.To.Add(email_addressTo);
            m.CC.Add(CCemailAdd);
            m.CC.Add(CCemailAdd2);
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

        public int getPatientIdKey(string patient_id)
        {
            int id_key;

            JEVEGA_UsDbEntities db = new JEVEGA_UsDbEntities();
            PatientData patientData = db.PatientDatas.Where(p => p.Patient_Id == patient_id).First();

            id_key = patientData.Id;
            return id_key;
        } //--

        public List<SelectListItem> CreateListMonthNames(int this_month)
        {
            bool nmonth;
            List<SelectListItem> listOfMonths = new List<SelectListItem>();

            if (this_month == 1) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo1 = new SelectListItem { Text = "January", Value = "1", Selected = nmonth };
            listOfMonths.Add(mo1);

            if (this_month == 2) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo2 = new SelectListItem { Text = "February", Value = "2", Selected = nmonth };
            listOfMonths.Add(mo2);

            if (this_month == 3) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo3 = new SelectListItem { Text = "March", Value = "3", Selected = nmonth };
            listOfMonths.Add(mo3);

            if (this_month == 4) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo4 = new SelectListItem { Text = "April", Value = "4", Selected = nmonth };
            listOfMonths.Add(mo4);

            if (this_month == 5) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo5 = new SelectListItem { Text = "May", Value = "5", Selected = nmonth };
            listOfMonths.Add(mo5);

            if (this_month == 6) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo6 = new SelectListItem { Text = "June", Value = "6", Selected = nmonth };
            listOfMonths.Add(mo6);

            if (this_month == 7) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo7 = new SelectListItem { Text = "July", Value = "7", Selected = nmonth };
            listOfMonths.Add(mo7);

            if (this_month == 8) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo8 = new SelectListItem { Text = "August", Value = "8", Selected = nmonth };
            listOfMonths.Add(mo8);

            if (this_month == 9) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo9 = new SelectListItem { Text = "September", Value = "9", Selected = nmonth };
            listOfMonths.Add(mo9);

            if (this_month == 10) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo10 = new SelectListItem { Text = "October", Value = "10", Selected = nmonth };
            listOfMonths.Add(mo10);

            if (this_month == 11) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo11 = new SelectListItem { Text = "November", Value = "11", Selected = nmonth };
            listOfMonths.Add(mo11);

            if (this_month == 12) { nmonth = true;  } else { nmonth = false; }
            SelectListItem mo12 = new SelectListItem { Text = "December", Value = "12", Selected = nmonth };
            listOfMonths.Add(mo12);

            return listOfMonths;
        } //--

        public List<SelectListItem> CreateListOfYears(int this_year)
        {
            bool nyear;
            List<SelectListItem> listOfYears = new List<SelectListItem>();

            if (this_year == 2019) { nyear = true; } else { nyear = false; }
            SelectListItem item1 = new SelectListItem { Text = "2019", Value = "2019", Selected = nyear };
            listOfYears.Add(item1);

            if (this_year == 2020) { nyear = true; } else { nyear = false; }
            SelectListItem item2 = new SelectListItem { Text = "2020", Value = "2020", Selected = nyear };
            listOfYears.Add(item2);

            if (this_year == 2021) { nyear = true; } else { nyear = false; }
            SelectListItem item3 = new SelectListItem { Text = "2021", Value = "2021", Selected = nyear };
            listOfYears.Add(item3);

            if (this_year == 2022) { nyear = true; } else { nyear = false; }
            SelectListItem item4 = new SelectListItem { Text = "2022", Value = "2022", Selected = nyear };
            listOfYears.Add(item4);

            if (this_year == 2023) { nyear = true; } else { nyear = false; }
            SelectListItem item5 = new SelectListItem { Text = "2023", Value = "2023", Selected = nyear };
            listOfYears.Add(item5);

            return listOfYears;
        } //--

    }
}