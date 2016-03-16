using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustEatCodeTestWeb.Services.Restaurants.JustEatRestaurantService
{
    public class RestaurantsByOutCodeResponse
    {
        public string ShortResultText { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}