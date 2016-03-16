using System;


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
            // TODO read from Web.config
            return new JustEatRestaurantServiceConfiguration()
            {
                Host = "public.je-apis.com",
                AcceptLanguage = "en-GB",
                AcceptTenant = "uk",
                AuthorizationParameter = "VGVjaFRlc3RBUEk6dXNlcjI=",
                AuthorizationScheme = "Basic",
                BaseAddress = "https://public.je-apis.com/restaurants",
                OutCodeParameterFormat = "?q={0}"
            };
        }
    }
}