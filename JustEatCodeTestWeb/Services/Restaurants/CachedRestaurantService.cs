using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;

namespace JustEatCodeTestWeb.Services.Restaurants
{
    public class CachedRestaurantService : IRestaurantService
    {
        private const string RESTAURANTS_CACHE_NAME = "RestaurantsByOutCode";
        private static MemoryCache _cache = new MemoryCache(RESTAURANTS_CACHE_NAME);
        private readonly IRestaurantService _restaurantServiceImplementation;
        private readonly int _cacheTimeoutSeconds;

        public CachedRestaurantService(IRestaurantService restaurantServiceImplementation, int cacheTimeoutSeconds = 10)
        {
            _restaurantServiceImplementation = restaurantServiceImplementation;
            _cacheTimeoutSeconds = cacheTimeoutSeconds;
        }

        public async Task<IEnumerable<IRestaurant>> GetByOutCodeAsync(string outCode)
        {
            var cacheKey = outCode == null ? null : outCode.Trim().ToUpperInvariant();

            var value = _cache.Get(cacheKey) as IEnumerable<IRestaurant>;

            if (value == null)
            {
                value = await _restaurantServiceImplementation.GetByOutCodeAsync(outCode);
                _cache.Set(cacheKey, value, DateTime.Now.AddSeconds(_cacheTimeoutSeconds));
            }

            return value;
        }

        public void Clear()
        {
            _cache.Dispose();
            _cache = new MemoryCache(RESTAURANTS_CACHE_NAME);
        }
    }
}