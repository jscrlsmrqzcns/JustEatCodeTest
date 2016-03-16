using System;
using System.Configuration;

namespace JustEatCodeTestWeb.Services.Restaurants.JustEatRestaurantService
{
    public class JustEatRestaurantServiceConfiguration : IJustEatRestaurantServiceConfiguration
    {
        public string AcceptLanguage { get; private set; }
        public string AcceptTenant { get; private set; }
        public string AuthorizationParameter { get; private set; }
        public string AuthorizationScheme { get; private set; }
        public string BaseAddress { get; private set; }
        public string Host { get; private set; }
        public string OutCodeParameterFormat { get; private set; }

        public static JustEatRestaurantServiceConfiguration FromApplicationConfig()
        {
            // TODO rather than AppSettings, use custom config section (cleaner)
            return new JustEatRestaurantServiceConfiguration()
            {                
                Host = ConfigurationManager.AppSettings["RestaurantServiceHost"],
                AcceptLanguage = ConfigurationManager.AppSettings["RestaurantServiceAcceptLanguage"],
                AcceptTenant = ConfigurationManager.AppSettings["RestaurantServiceAcceptTenant"],
                AuthorizationParameter = ConfigurationManager.AppSettings["RestaurantServiceAuthorizationParameter"],
                AuthorizationScheme = ConfigurationManager.AppSettings["RestaurantServiceAuthorizationScheme"],
                BaseAddress = ConfigurationManager.AppSettings["RestaurantServiceBaseAddress"],
                OutCodeParameterFormat = ConfigurationManager.AppSettings["RestaurantServiceOutCodeParameterFormat"]
            };
        }
    }
}