using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Configuration;
using Clockwork.Web.DAL;
using Clockwork.Web.Models;


namespace Clockwork.Web.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CurrentTimeQueries"].ConnectionString;

        public ActionResult Index(string id)
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            CurrentTimeQuerySqlDAL currentTimeQuerySql = new CurrentTimeQuerySqlDAL(connectionString);
            List<CurrentTimeQuery> ctq = currentTimeQuerySql.GetCurrentTimeQuery();

            //id used to indicate whether time zone box is shown or not
            if (String.IsNullOrEmpty(id))
            {
                return View("Index", ctq);
            }
            else
            {
                return View("GetTime", ctq);
            }
            
        }

    }
}
