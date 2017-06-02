using System;
using System.Collections.Generic;
using System.Linq;
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
        public int TheStore { get; set; }
        public int Gender { get; set; }
        public string GroupId { get; set; }
        public Position Position { get; set; }
        public double Price { get; set; }
        public string Label { get; set; }
        public int TheColor { get; set; }
        public int TheType { get; set; }

        public override string ToString()
        {
            return $"Product {ProductId} with style {StyleId}";
        }
        public List<SelectListItem> getTypes()
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.kiabi.com/") })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-APIKey", "4a609e00-46aa-11e7-98c3-9ed7d2a714b7");
                var response = client.GetAsync($"v1/products").Result;
                var result = response.IsSuccessStatusCode ? response.Content.ReadAsAsync<IEnumerable<TypeProdcut>>().Result : null;
                List<SelectListItem> myList = new List<SelectListItem>();

                foreach (var i in result)
                {
                    var data = new[] { new SelectListItem { Value = i.Type, Text = i.Type } };

                    myList.Add(data.FirstOrDefault());
                }

                return myList;
            }
        }

        public List<SelectListItem> getColors()
        {

            using (var client = new HttpClient { BaseAddress = new Uri("https://api.kiabi.com/") })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-APIKey", "b10fd700-475f-11e7-98c3-ea1aec61695a");
                var response = client.GetAsync($"v1/style_parameters/base_colors").Result;
                var result = response.IsSuccessStatusCode ? response.Content.ReadAsAsync<IEnumerable<BaseColor>>().Result : null;
                List<SelectListItem> myList = new List<SelectListItem>();

                foreach (var i in result)
                {
                    var data = new[] { new SelectListItem { Value = i.ColorId, Text = i.ColorLabel } };

                    myList.Add(data.FirstOrDefault());
                }

                return myList;
            }

        }

        public List<SelectListItem> getGroups()
        {

            using (var client = new HttpClient { BaseAddress = new Uri("https://api.kiabi.com/") })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-APIKey", "6d750660-46aa-11e7-98c3-cf14e2f8b294");
                var response = client.GetAsync($"v1/styles/hierarchies/product/hierarchy?fields=children(id,label)").Result;
                var res = response.Content.ReadAsStringAsync().Result;
                var result = response.IsSuccessStatusCode ? response.Content.ReadAsAsync<IEnumerable<Child>>().Result : null;
                List<SelectListItem> myList = new List<SelectListItem>();
                //var data = new[] { new SelectListItem()};
                foreach (var i in result.FirstOrDefault().children)
                {
                    var data = new[] { new SelectListItem { Value = i.Id, Text = i.Label } };

                    myList.Add(data.FirstOrDefault());
                }

                //myList = data.ToList();
                return myList;
            }
        }

        public List<SelectListItem> getStores()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="1",Text="Villeneuve D'ascq"},
                 new SelectListItem{ Value="2",Text="Lille/Euralille"},


             };
            myList = data.ToList();
            return myList;
        }

        static public IEnumerable<Product> GetProducts(string groupId = null, string type = null, string baseColorId = null, string shop = null)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.kiabi.com/") })
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

        static public Product GetProduct(string productId)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.kiabi.com/") })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-APIKey", "4a609e00-46aa-11e7-98c3-9ed7d2a714b7");

                var response = client.GetAsync($"v1/products/{productId}").Result;
                var result = response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Product>().Result : null;

                client.DefaultRequestHeaders.Remove("X-APIKey");
                client.DefaultRequestHeaders.Add("X-APIKey", "6d750660-46aa-11e7-98c3-cf14e2f8b294");

                if (result == null)
                {
                    return null;
                }

                var stylesResponse = client.GetAsync($"v1/styles/{result.StyleId}").Result;
                var styleResult = stylesResponse.IsSuccessStatusCode
                    ? stylesResponse.Content.ReadAsAsync<Style>().Result
                    : null;
                result.GroupId = styleResult?.GetGroupId();
                result.Position = styleResult?.GetPosition(result.ColorId);
                result.Label = styleResult?.ShortTitle;
                result.GetPrice();
                result.Picture = $"{result.Picture}?apikey=HACKATHON";

                return result;
            }
        }

        public void GetPrice()
        {
            var skuIdList = Sizes.Select(price => price.Sku.SkuId);
            foreach (var skuId in skuIdList)
            {
                using (var client = new HttpClient { BaseAddress = new Uri("https://api.kiabi.com/") })
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("X-APIKey", "f4f5cd90-4768-11e7-98c3-93eab5a77343");

                    var response = client.GetAsync($"v1/prices/initial_prices/{skuId}").Result;
                    var result = response.IsSuccessStatusCode ? response.Content.ReadAsAsync<PriceList>().Result : null;

                    var price = result?.Prices?.First(item => item.CountryId == "FR").InitialPriceIncludingVat;
                    if (price != null)
                    {
                        Price = (double)price;
                        return;
                    }
                }
            }
            Price = 0;
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

    public class Price
    {
        public string CountryId { get; set; }
        public string CurrencyId { get; set; }
        public double InitialPriceIncludingVat { get; set; }
    }

    public class PriceList
    {
        public int SkuId { get; set; }
        public List<Price> Prices { get; set; }
    }


    public class BaseColor
    {
        public string ColorId { get; set; }
        public string ColorLabel { get; set; }
    }

    public class TypeProdcut
    {
        public string Type { get; set; }
        
    }

    public class ProductGroup
    {
        public string Id { get; set; }
        public string Label { get; set; }
    }

    public class Child
    {
        public List<ProductGroup> children { get; set; }

        public Child()
        {
            children = new List<ProductGroup>();
        }
    }
}