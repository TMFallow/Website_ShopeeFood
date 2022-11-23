using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeFood_Repository;
using ShopeeFood_Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IType_Services type_Services;

        private readonly ILogger<UsersController> logger;

        Application_Context db;

        public TypesController(IType_Services type_Services, ILogger<UsersController> logger)
        {
            this.type_Services = type_Services;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Types> GetTypes()
        {
            return type_Services.GetAllTypes().ToArray();
        }

        [HttpGet("GetTypeFromID")]
        public IEnumerable<Types> GetTypesByID(int AreaID)
        {
            List<Types> types = new List<Types>();

            var ListType = GetTypes();

            foreach (Types item in ListType)
            {
                if (item.ID == AreaID)
                {
                    types.Add(item);
                }
            }
            return types;
        }

        [HttpGet("getNameTypeByIDOnRestaurant/{RestaurantID:int}")]
        public IEnumerable<Types> getNameTypeByIDOnRestaurant(int RestaurantID)
        {
            List<Types> types = new List<Types>();

            //var listType = 

            ////Thêm dô vào List<Types>

            //foreach (Types item in data)
            //{
            //    if (item.ID == RestaurantID)
            //    {
            //        types.Add(item);
            //    }
            //}

            return types;
        }
    }
}
