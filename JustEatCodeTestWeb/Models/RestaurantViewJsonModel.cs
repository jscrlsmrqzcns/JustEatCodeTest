using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T4TS;

namespace JustEatCodeTestWeb.Models
{
    [TypeScriptInterface]
    public class RestaurantViewJsonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public IEnumerable<string> CusineTypes { get; set; }
        public string LogoUrl { get; set; }
    }
}