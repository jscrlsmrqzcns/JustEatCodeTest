using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustEatCodeTestWeb.Services.Restaurants.JustEatRestaurantService
{
    public interface IJustEatRestaurantServiceConfiguration
    {
        string BaseAddress { get; }
        string AcceptTenant { get; }
        string AcceptLanguage { get; }
        string AuthorizationScheme { get; }
        string AuthorizationParameter { get; }
        string Host { get; }
        string OutCodeParameterFormat { get; }
    }
}
