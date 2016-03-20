using JustEatCodeTestWeb.Services.Restaurants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustEatCodeTestWeb.Tests.Services.Restaurants
{
    [TestClass]
    public class CachedRestaurantServiceTest
    {
        [TestInitialize]
        public void ClearCache()
        {
            var cachedRestaurantService = new CachedRestaurantService(null, 10);
            cachedRestaurantService.Clear();
        }

        [TestMethod]
        public void TestReturnsItemCached()
        {
            var restaurantServiceImplTestCheck = new RestaurantServiceImplTestCheck();
            var cachedRestaurantService = new CachedRestaurantService(restaurantServiceImplTestCheck, 10);

            var one1 = cachedRestaurantService.GetByOutCodeAsync("one").Result;
            var one2 = cachedRestaurantService.GetByOutCodeAsync("one").Result;
            var one3 = cachedRestaurantService.GetByOutCodeAsync("one").Result;

            Assert.AreEqual(1, restaurantServiceImplTestCheck.Requests.Count());
        }

        [TestMethod]
        public void TesExpiration()
        {
            var restaurantServiceImplTestCheck = new RestaurantServiceImplTestCheck();
            var cachedRestaurantService = new CachedRestaurantService(restaurantServiceImplTestCheck, 3);

            var oneNew = cachedRestaurantService.GetByOutCodeAsync("one").Result;
            var oneCached1 = cachedRestaurantService.GetByOutCodeAsync("one").Result;

            Assert.AreEqual(1, restaurantServiceImplTestCheck.Requests.Count());
            Assert.AreEqual(oneNew, oneCached1);

            Thread.Sleep(4*1000);

            var oneExpired = cachedRestaurantService.GetByOutCodeAsync("one").Result;
            var oneCached2 = cachedRestaurantService.GetByOutCodeAsync("one").Result;
            Assert.AreEqual(oneExpired, oneCached2);
            Assert.AreEqual(2, restaurantServiceImplTestCheck.Requests.Count());
        }

        [TestMethod]
        public void TestSingleCacheInstance()
        {
            var restaurantServiceImplTestCheck = new RestaurantServiceImplTestCheck();
            var cachedRestaurantService = new CachedRestaurantService(restaurantServiceImplTestCheck, 30);

            var oneNew = cachedRestaurantService.GetByOutCodeAsync("one").Result;
            var oneCached1 = cachedRestaurantService.GetByOutCodeAsync("one").Result;

            Assert.AreEqual(1, restaurantServiceImplTestCheck.Requests.Count());
            cachedRestaurantService = new CachedRestaurantService(restaurantServiceImplTestCheck, 30);

            var oneCached2 = cachedRestaurantService.GetByOutCodeAsync("one").Result;
            Assert.AreEqual(1, restaurantServiceImplTestCheck.Requests.Count());
        }
    }

    internal class RestaurantServiceImplTestCheck : IRestaurantService
    {
        public List<string> Requests { get; private set; }

        public RestaurantServiceImplTestCheck()
        {
            Requests = new List<string>();
        }

        public async Task<IEnumerable<IRestaurant>> GetByOutCodeAsync(string outCode)
        {
            Requests.Add(outCode);

            return new List<IRestaurant>() { Mock.Of<IRestaurant>() };
        }
    }
}
