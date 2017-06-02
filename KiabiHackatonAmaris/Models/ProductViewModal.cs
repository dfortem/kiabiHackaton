using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiabiHackatonAmaris.Models
{
    public class ProductViewModal
    {

        public List<Product> PagedList { get; set; }
        public AdvancedSearchModel SearchQuery { get; set; }
    }
}