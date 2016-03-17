using JustEatCodeTestWeb.Models;
using JustEatCodeTestWeb.Services.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JustEatCodeTestWeb.Controllers
{
    public class RestaurantsApiController : ApiController
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsApiController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<IEnumerable<RestaurantViewJsonModel>> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Outcode not provided.");

            var restaurants = await _restaurantService.GetByOutCodeAsync(id);

            return restaurants.Select(r => new Models.RestaurantViewJsonModel()
            {
                Id = r.Id,
                Name = r.Name,
                Rating = r.Rating,
                CusineTypes = r.CusineTypes,
                LogoUrl = r.LogoUrl
            }).GroupBy(r => r.Id).Select(g => g.First()); // filter repeated
        }
    }
}
