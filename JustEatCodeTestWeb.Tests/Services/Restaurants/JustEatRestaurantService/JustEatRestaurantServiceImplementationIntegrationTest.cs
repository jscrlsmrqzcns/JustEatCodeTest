using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using JustEatCodeTestWeb.Services.Restaurants.JustEatRestaurantService;

namespace JustEatCodeTestWeb.Tests.Services.Restaurants.JustEatRestaurantService
{
    [TestClass]
    public class JustEatRestaurantServiceImplementationIntegrationTest
    {
        [TestCategory("Integration")]
        [TestMethod]
        public void GetByOutCodeSE19Test() // conectivity testing, finding out if service works without having to open the application
        {

            var serviceConfiguration = Mock.Of<IJustEatRestaurantServiceConfiguration>(
                                                       m => m.Host == "public.je-apis.com"
                                                    && m.AcceptLanguage == "en-GB"
                                                    && m.AcceptTenant == "uk"
                                                    && m.AuthorizationParameter == "VGVjaFRlc3RBUEk6dXNlcjI="
                                                    && m.AuthorizationScheme == "Basic"
                                                    && m.BaseAddress == "https://public.je-apis.com/restaurants"
                                                    && m.OutCodeParameterFormat == "?q={0}");

            var justEatRestaurantService = new JustEatRestaurantServiceImplementation(serviceConfiguration);

            var se19Restaurants = justEatRestaurantService.GetByOutCodeAsync("SE19").Result;
            Assert.IsNotNull(se19Restaurants);
            Assert.IsTrue(se19Restaurants.Count() > 0, "Either something is wrong with the service, or all restaurants around SE19 have closed!");
        }


        // TODO mock HttpClient and use static json to test service conversion
    }
}
