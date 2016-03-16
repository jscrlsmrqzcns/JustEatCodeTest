using System.Collections.Generic;

namespace JustEatCodeTestWeb.Services.Restaurants
{
    public interface IRestaurant // specify only the properties that we need
    {
        string Name { get; }
        decimal? Rating { get; }
        IEnumerable<string> CusineTypes { get; }
    }
}