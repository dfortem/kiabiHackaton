using KiabiHackatonAmaris.EnumClass;
using KiabiHackatonAmaris.Models;
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

            Product objModel = new Product();
            objModel.Stores = objModel.getStores();
            objModel.Groups = objModel.getGroups();
            objModel.Colors = objModel.getColors();
            objModel.Types = objModel.getTypes();
            return View(objModel);
           
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