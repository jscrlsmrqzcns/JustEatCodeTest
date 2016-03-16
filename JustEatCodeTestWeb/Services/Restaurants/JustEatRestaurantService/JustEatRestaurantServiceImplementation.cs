﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace JustEatCodeTestWeb.Services.Restaurants.JustEatRestaurantService
{
    public class JustEatRestaurantServiceImplementation : IRestaurantService
    {
        private readonly IJustEatRestaurantServiceConfiguration _serviceConfiguration;

        public JustEatRestaurantServiceImplementation(IJustEatRestaurantServiceConfiguration serviceConfiguration)
        {
            // Accept configuration as a parameter as it will change in different environments
            // Also, in some cases it will be possible to adapt to service changes with a simple configuration adjustment
            _serviceConfiguration = serviceConfiguration;
        }

        public async Task<IEnumerable<IRestaurant>> GetByOutCodeAsync(string outCode)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_serviceConfiguration.BaseAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Host = _serviceConfiguration.Host;
                    client.DefaultRequestHeaders.Add("Accept-Tenant", _serviceConfiguration.AcceptTenant);
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(_serviceConfiguration.AcceptLanguage));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_serviceConfiguration.AuthorizationScheme,
                                                                                               _serviceConfiguration.AuthorizationParameter);                    

                    var result = await client.GetAsync(string.Format(_serviceConfiguration.OutCodeParameterFormat, outCode));

                    if (result.IsSuccessStatusCode)
                    {
                        var restaurantsByOutCodeResponse = await result.Content.ReadAsAsync<RestaurantsByOutCodeResponse>();

                        if (restaurantsByOutCodeResponse == null || restaurantsByOutCodeResponse.Restaurants == null)
                            return new List<IRestaurant>(0);


                        return restaurantsByOutCodeResponse.Restaurants.Select(r => new RestaurantResult()
                        {
                            Name = r.Name,
                            Rating = r.RatingStars,
                            CusineTypes = r.CuisineTypes == null ? null : r.CuisineTypes.Select(ct => ct.Name)
                        });
                    }
                    else
                    {
                        throw new Exception(string.Format("({0}): {1}", (int)result.StatusCode, result.ReasonPhrase));
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO timeout etc
                throw ex; // this is bad
            }
        }


        private class RestaurantResult : IRestaurant
        {
            public string Name { get; set; }
            public decimal? Rating { get; set; }
            public IEnumerable<string> CusineTypes { get; set; }
        }
    }
}