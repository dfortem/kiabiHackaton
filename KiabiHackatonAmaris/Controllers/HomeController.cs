using KiabiHackatonAmaris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KiabiHackatonAmaris.EnumClass;


namespace KiabiHackatonAmaris.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var p1 = Product.GetProduct("CC062_1");
            var p2 = Product.GetProduct("CC066_1");
            var p3 = Product.GetProduct("AY068_3");
            var p4 = Product.GetProduct("BG869_14");
            var p5 = Product.GetProduct("BN359_1");
            var p6 = Product.GetProduct("CC331_27");
            var p7 = Product.GetProduct("AQ482_7");
            var p8 = Product.GetProduct("CC331_16");


            ProductViewModal model = new ProductViewModal();
            model.PagedList.Add(p1);
            model.PagedList.Add(p2);
            model.PagedList.Add(p3);
            model.PagedList.Add(p4);
            model.PagedList.Add(p5);
            model.PagedList.Add(p6);
            model.PagedList.Add(p7);
            model.PagedList.Add(p8);

            
            return View(model);
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var res = Product.GetProduct("AE055_3");

            return View(res.ToString() ?? "");
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