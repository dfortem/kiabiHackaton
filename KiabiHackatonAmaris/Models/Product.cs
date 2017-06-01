using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace KiabiHackatonAmaris.Models
{

    public class Product
    {
        public string ProductId { get; set; }
        public string StyleId { get; set; }
        public string ColorId { get; set; }
        public string Picture { get; set; }
        public string BaseColorId { get; set; }
        public string BaseColorLabel { get; set; }
        public List<int> Rgb { get; set; }
        public string BrandId { get; set; }
        public string BrandLabel { get; set; }
        public string SizeGrid { get; set; }
        public string Type { get; set; }
        public List<Size> Sizes { get; set; } 
        public List<SelectListItem> Stores { get; set; }
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> Colors { get; set; }
        public List<SelectListItem> Types { get; set; }

        public int gender { get; set; }
        public override string ToString()
        {
            return $"Product {ProductId} with style {StyleId}";
        }
        public List<SelectListItem> getGroups()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="1",Text="Homme"},
                 new SelectListItem{ Value="2",Text="Femme"},
                 new SelectListItem{ Value="3",Text="Jeune"},
                 new SelectListItem{ Value="4",Text="Bébé"},

             };
            myList = data.ToList();
            return myList;
        }

        public List<SelectListItem> getStores()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="1",Text="Villeneuve D'ascq"},
                 new SelectListItem{ Value="2",Text="Lille/Euralille"},
                 new SelectListItem{ Value="3",Text="Lens"},
                 new SelectListItem{ Value="4",Text="Arras"},
                 
             };
            myList = data.ToList();
            return myList;
        }
        static public IEnumerable<Product> GetProducts(string groupId = null, string type = null, string baseColorId = null, string shop = null)
        {
            using (var client = new HttpClient {BaseAddress = new Uri("https://api.kiabi.com/")})
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-APIKey", "4a609e00-46aa-11e7-98c3-9ed7d2a714b7");

                //HttpResponseMessage response;
                var queryString = "";
                if (baseColorId != null)
                {
                    var query = HttpUtility.ParseQueryString(string.Empty);
                    query["baseColorId"] = "NOIR";
                    queryString = $"?{query}";
                }

                var response = client.GetAsync($"v1/products{queryString}").Result;
                return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<IEnumerable<Product>>().Result : null;
            }
        }
    }

    public class Size
    {
        public string SizeId { get; set; }
        public Sku Sku { get; set; }
    }

    public class Sku
    {
        public int SkuId { get; set; }
        public string Ean { get; set; }
        public double Weight { get; set; }
        public bool Active { get; set; }
    }

   
}