using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KiabiHackatonAmaris.Models;

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
            var res = Product.GetProducts();
            var result = res?.Select(item => item.ToString());

            return View(result ?? new List<string>());
        }
    }
}