using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustEatCodeTestWeb.Models
{
    public class RestaurantViewModel
    {
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public IEnumerable<string> CusineTypes { get; set; }
    }
}