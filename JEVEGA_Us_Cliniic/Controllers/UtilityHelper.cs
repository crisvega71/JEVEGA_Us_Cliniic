using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}