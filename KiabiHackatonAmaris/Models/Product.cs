using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace KiabiHackatonAmaris.Models
{

    public class Product
    {
        public string productId { get; set; }
        public string styleId { get; set; }
        public string colorId { get; set; }
        public string picture { get; set; }
        public string baseColorId { get; set; }
        public string baseColorLabel { get; set; }
        public List<int> rgb { get; set; }
        public string brandId { get; set; }
        public string brandLabel { get; set; }
        
        public string sizeGrid { get; set; }
        public string type { get; set; }
        public List<Size> sizes { get; set; } 
        public List<SelectListItem> stores { get; set; }
        public List<SelectListItem> groups { get; set; }
        public List<SelectListItem> colors { get; set; }
        public List<SelectListItem> types { get; set; }
        public int theStore { get; set; }

        public int gender { get; set; }
        public override string ToString()
        {
            return $"Product {productId} with style {styleId}";
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
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.kiabi.com/v1") })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-APIKey", "4a609e00-46aa-11e7-98c3-9ed7d2a714b7");

                dynamic filter = new JObject();

                filter.List = new List<Dictionary<string, string>>();

                if (groupId != null)
                {
                    var selector = new Dictionary<string, string>
                                    {
                                        { "field", "groupId" },
                                        { "operator", "EQ" },
                                        {"value", groupId }
                                    };
                    filter.Add(selector);
                }

                if (type != null)
                {
                    var selector = new Dictionary<string, string>
                                    {
                                        { "field", "type" },
                                        { "operator", "EQ" },
                                        {"value", type }
                                    };
                    filter.Add(selector);
                }

                if (baseColorId != null)
                {
                    var selector = new Dictionary<string, string>
                                    {
                                        { "field", "baseColorId" },
                                        { "operator", "EQ" },
                                        {"value", baseColorId }
                                    };
                    filter.Add(selector);
                }

                HttpResponseMessage response;
                if (filter.Any())
                {
                    var content = new FormUrlEncodedContent(filter);
                    response = client.PostAsync("search", content).Result;
                }
                else
                {
                    response = client.GetAsync("").Result;
                }
                return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<IEnumerable<Product>>().Result : null;
            }
        }
    }

    public class Size
    {
        public string sizeId { get; set; }
        public Sku sku { get; set; }
    }

    public class Sku
    {
        public int skuId { get; set; }
        public string ean { get; set; }
        public double weight { get; set; }
        public bool active { get; set; }
    }

   
}