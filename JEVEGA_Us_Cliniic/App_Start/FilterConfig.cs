using System.Web;
using System.Web.Mvc;

namespace JEVEGA_Us_Cliniic
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
