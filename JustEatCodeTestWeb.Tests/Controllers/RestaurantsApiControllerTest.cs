using JustEatCodeTestWeb.Controllers;
using JustEatCodeTestWeb.Models;
using JustEatCodeTestWeb.Services.Restaurants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustEatCodeTestWeb.Tests.Controllers
{
    [TestClass]
    public class RestaurantsApiControllerTest
    {
        [TestMethod]
        public void Get_Empty()
        {
            var restaurantServiceMock = new Mock<IRestaurantService>();
            restaurantServiceMock.Setup(rs => rs.GetByOutCodeAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((IEnumerable<IRestaurant>)new List<IRestaurant>()));

            var restaurantApiController = new RestaurantsApiController(restaurantServiceMock.Object);
            Assert.AreEqual(0, restaurantApiController.Get("foo").Result.Count());
        }

        [TestMethod]
        public void Get_Data()
        {
            var restaurant1 = Mock.Of<IRestaurant>(r => r.Id == 1 && r.Name == "Pepe tapas" && r.Rating == 4.9m
            && r.CusineTypes == new string[] { "spanish", "andalusian" } );
            var restaurant2 = Mock.Of<IRestaurant>(r => r.Id == 2 && r.Name == "Juan kitchen" && r.Rating == 0.0m);

            var restaurantServiceMock = new Mock<IRestaurantService>();
            restaurantServiceMock.Setup(rs => rs.GetByOutCodeAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((IEnumerable<IRestaurant>)new List<IRestaurant>()
                {
                    restaurant1, restaurant2
                }));
                
            var restaurantApiController = new RestaurantsApiController(restaurantServiceMock.Object);
            var restaurants = restaurantApiController.Get("foo").Result;
            Assert.AreEqual(2, restaurants.Count());

            AssertRestaurantEquals(restaurant1, restaurants.Single(r => r.Id == 1));
            AssertRestaurantEquals(restaurant2, restaurants.Single(r => r.Id == 2));
        }

        [TestMethod]
        public void Get_Distinct()
        {
            var restaurant1 = Mock.Of<IRestaurant>(r => r.Id == 1 && r.Name == "Pepe tapas" && r.Rating == 4.9m
            && r.CusineTypes == new string[] { "spanish", "andalusian" });
            var restaurant2 = Mock.Of<IRestaurant>(r => r.Id == 2 && r.Name == "Juan kitchen" && r.Rating == 0.0m);

            var restaurantServiceMock = new Mock<IRestaurantService>();
            restaurantServiceMock.Setup(rs => rs.GetByOutCodeAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((IEnumerable<IRestaurant>)new List<IRestaurant>()
                {
                    restaurant1, restaurant2, restaurant1
                }));

            var restaurantApiController = new RestaurantsApiController(restaurantServiceMock.Object);
            var restaurants = restaurantApiController.Get("foo").Result;
            Assert.AreEqual(2, restaurants.Count());

            AssertRestaurantEquals(restaurant1, restaurants.Single(r => r.Id == 1));
            AssertRestaurantEquals(restaurant2, restaurants.Single(r => r.Id == 2));
        }

        [TestMethod]
        public void Get_DistinctCusine()
        {
            var restaurant1 = Mock.Of<IRestaurant>(r => r.Id == 1 && r.Name == "L'Entrecote" && r.Rating == 4.9m
            && r.CusineTypes == new string[] { "French", "parisian", "french" });

            var restaurantServiceMock = new Mock<IRestaurantService>();
            restaurantServiceMock.Setup(rs => rs.GetByOutCodeAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((IEnumerable<IRestaurant>)new List<IRestaurant>()
                {
                    restaurant1
                }));

            var restaurantApiController = new RestaurantsApiController(restaurantServiceMock.Object);
            var restaurants = restaurantApiController.Get("foo").Result;
            Assert.AreEqual(2, restaurants.Single().CusineTypes.Count());
            Assert.AreEqual(1, restaurants.Single().CusineTypes.Count(c => "french".Equals(c, StringComparison.InvariantCultureIgnoreCase)));
            Assert.AreEqual(1, restaurants.Single().CusineTypes.Count(c => "parisian".Equals(c, StringComparison.InvariantCultureIgnoreCase)));
        }

        private void AssertRestaurantEquals(IRestaurant rx, RestaurantViewJsonModel ry)
        {
            Assert.AreEqual(rx.Id, ry.Id);
            Assert.AreEqual(rx.LogoUrl, ry.LogoUrl);
            Assert.AreEqual(rx.Name, ry.Name);
            Assert.AreEqual(rx.Rating, ry.Rating);
            Assert.IsTrue((rx.CusineTypes == null && ry.CusineTypes == null) || rx.CusineTypes.SequenceEqual(ry.CusineTypes));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Outcode not provided.")]
        public void Get_MissingParameterTest()
        {
            var restaurantServiceMock = new Mock<IRestaurantService>();
            restaurantServiceMock.Setup(rs => rs.GetByOutCodeAsync(It.IsAny<string>())).Throws(new Exception("shouldn't even call me"));

            var restaurantApiController = new RestaurantsApiController(restaurantServiceMock.Object);
            try
            {
                restaurantApiController.Get(string.Empty).Wait();
            }
            catch (AggregateException ae)
            {
                throw ae.InnerExceptions.First();
            }
        }
    }
}
