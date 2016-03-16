using JustEatCodeTestWeb.Services.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JustEatCodeTestWeb.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // Note: Async controller using async service to avoid blocking worker thread
        public async Task<ActionResult> GetByOutCode(string outCode)
        {
            var restaurants = await _restaurantService.GetByOutCodeAsync(outCode);

            // TODO order by proximity/rating then alphabetically? 
            // TODO allow sorting in UI?
            return View("Index", restaurants.Select(r => new Models.RestaurantViewModel()
            {
                Name = r.Name,
                Rating = r.Rating,
                CusineTypes = r.CusineTypes
            }));
        }
    }
}