using System.Web;
using System.Web.Mvc;

namespace Capstone_ECommerce_progject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
