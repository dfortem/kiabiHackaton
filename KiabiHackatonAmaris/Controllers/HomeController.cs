using KiabiHackatonAmaris.EnumClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace KiabiHackatonAmaris.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetStores()
        {


            var data = Enum.GetValues(typeof(StoreEnum)).Cast<StoreEnum>().Select(e => new
            {
                id = (int)e,
                text = e.ToString()
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}