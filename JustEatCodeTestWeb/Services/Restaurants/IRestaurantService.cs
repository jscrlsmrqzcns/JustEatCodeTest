using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustEatCodeTestWeb.Services.Restaurants
{
    public interface IRestaurantService // Decouple from server implementation
    {
        Task<IEnumerable<IRestaurant>> GetByOutCodeAsync(string outCode);
    }
}
