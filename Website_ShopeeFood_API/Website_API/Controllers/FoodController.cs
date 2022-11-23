using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopeeFood_Repository;
using ShopeeFood_Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFood_Services food;

        private readonly ILogger<FoodController> logger;

        private readonly Application_Context application_Context;

        public FoodController(IFood_Services food, ILogger<FoodController> logger, Application_Context application_Context)
        {
            this.food = food;
            this.logger = logger;
            this.application_Context = application_Context;
        }

        [HttpGet]
        public IEnumerable<Foods> GetAllFoods()
        {
            return food.GetAllFoods();
        }

        [HttpGet("getAllRestaurantUsingIDRestaurant/{Id:int}")]
        public IEnumerable<Foods> getAllRestaurantUsingIDRestaurant(int Id)
        {
            List<Foods> listFood = new List<Foods>();

            var list = food.getByID(Id);

            foreach (Foods item in list)
            {
                if (item.RestaurantID == Id)
                {
                    listFood.Add(item);
                }
            }
            return listFood;
        }

        [HttpGet("getListRestaurantBasedOnTypeId/{TypeofFood}")]
        public IEnumerable<Foods> getListRestaurantBasedOnTypeId(int TypeofFood)
        {
            List<Foods> listFoods = new List<Foods>();

            //List<Foods> data = application_Context.foods
            //                  .AsEnumerable()
            //                  .Where(x => x.TypeofFood == TypeofFood)
            //                  .GroupBy(x => x.RestaurantID)
            //                  .Cast<Foods>().ToList();

            var listFood = food.getByIdType(TypeofFood);

            foreach (var item in listFood)
            {
                listFoods.Add(item);
            }

            return listFoods;

        }
    }
}
