using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeFood_Services.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurant_Services restaurant_Services;

        private readonly ILogger<RestaurantController> logger;

        public RestaurantController(IRestaurant_Services restaurant_Services, ILogger<RestaurantController> logger)
        {
            this.restaurant_Services = restaurant_Services;
            this.logger = logger;
        }

        [HttpGet("GetRestaurant")]
        public IEnumerable<Restaurant> GetRestaurants()
        {
            return restaurant_Services.GetAllRestaurant().ToArray();
        }

        [HttpGet("GetRestaurantByID")]
        public IEnumerable<Restaurant> GetRestaurantByID(int AreaID)
        {
            List<Restaurant> rest = new List<Restaurant>();

            var ListRes = GetRestaurants();

            foreach (Restaurant restaurant in ListRes)
            {
                if (restaurant.AreaID == AreaID)
                {
                   rest.Add(restaurant);
                }
            }
            return rest;
        }


        [HttpGet("GetRestaurantByTypeID")]
        public IEnumerable<Restaurant> GetRestaurantByTypeID(int ID)
        {
            List<Restaurant> rest = new List<Restaurant>();

            var ListRes = GetRestaurants();

            foreach (Restaurant restaurant in ListRes)
            {
                if (restaurant.ID == ID)
                {
                    rest.Add(restaurant);
                }
            }
            return rest;
        }

        [HttpGet("GetIDOfRestaurant/{restaurantId:int}")]
        public Restaurant GetIDOfRestaurant(int restaurantId)
        {
            return restaurant_Services.GetRestaurantByID(restaurantId);
        }

        [HttpGet("GetListRestaurant/{Id:int}")]
        public Restaurant getListRestaurantByIdType(int Id)
        {
            return restaurant_Services.GetRestaurantByID(Id);
        }

        [HttpGet("getListOfrestaurantByIdDistricts/{IDDetailsArea:int}")]
        public IEnumerable<Restaurant> getListOfrestaurantByIdDistricts(int IDDetailsArea)
        {
            return restaurant_Services.getListOfRestaurantByIdDistricts(IDDetailsArea);
        }

        [HttpGet("getListOfRestaurantByIdTypes/{IdTypes:int}")]
        public IEnumerable<Restaurant> getListOfRestaurantByIdTypes(int IdTypes)
        {
            return restaurant_Services.getListOfRestaurantByIdTypes(IdTypes);
        }

    }
}
